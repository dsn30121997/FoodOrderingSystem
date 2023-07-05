using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Controllers
{
    public class OrdersController : Controller
    {
        private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();




        // For Admin
        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
             var orders1 = orders.OrderByDescending(o => o.OrderDate);
            return View(await orders.ToListAsync());
        }





        //For getting All Order By single customer
        // Get Orders/MyOrders/2
        // Get Orders/MyOrders/Customer Id
        public ActionResult MyOrders(int CustomerId)
        {
            Customer customer = db.Customer.Find(CustomerId);
            List<Orders> orders = db.Orders.Where(c => c.CustomerId == CustomerId).ToList();
            return View(orders);
        }



        //For getting Orders Details
        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = await db.Orders.FindAsync(id);
            List<Orders> order = db.Orders.Where(o => o.CustomerId == id).ToList();
            if (orders == null)
            {
                return View(orders); ;
            }
            return View(orders);
        }




        // For getting Customers Instruction
        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            return View();
        }






        // Creating New order Adding Data to Order Table
        // POST: Orders/Create
        // GET: Orders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderId,CustomerId,TotalAmount,OrderDate,OrderStatus,SpicialInstruction")] Orders orders)
        {
            //Check if user is logged in
            if (Session["Id"] != null)
            {
                int CustomerId = (int)Session["Id"];
                Customer customer = await db.Customer.FindAsync(CustomerId);
                //Getting complet List Of items from Cart
                List<Cart> carts = db.Cart.Where(c => c.CustomerId == CustomerId).ToList();

                //Check if cart is empty
                if (carts.Count == 0)
                {
                    ViewBag.Error = "At lease add one item to your cart before placing an order.";
                    return View("Create");
                }

                MenuList menuList = new MenuList();

                orders.CustomerId = customer.CustomerId;
                // Logic for Total Amount that need to pay 
                // Ordered Total Price
                foreach (var item in carts)
                {
                    orders.TotalAmount += item.TotalAmount;
                }
                orders.OrderStatus = "Recived Order";
                db.Orders.Add(orders);
                orders.OrderDate = DateTime.Now;
                await db.SaveChangesAsync();

                //Adding Each Item in Ordered Items and               
                foreach (var item in carts)
                {
                    OrderItems orderItems = new OrderItems();
                    orderItems.OrderId = orders.OrderId;
                    orderItems.ItemId = item.ItemId;
                    orderItems.Quantity = item.Quantity;
                    orderItems.ItemStatus = "Recived Order";
                    db.Entry(orderItems).State = EntityState.Added;
                    db.SaveChanges();

                    //Deleting Items From Cart
                    db.Cart.Remove(db.Cart.Find(CustomerId, item.ItemId));
                    db.SaveChanges();
                }
                
                
                Payment payment = new Payment();
                payment.OrderId = orders.OrderId;
                payment.TotalAmount = orders.TotalAmount;
                return RedirectToAction("Create", "Payments", new { OrderId = orders.OrderId });


            }
            return View();
    }


    //For Admin
    // Getting Edit Page
    // GET: Orders/Edit/5
    public async Task<ActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Orders orders = await db.Orders.FindAsync(id);
        if (orders == null)
        {
            return HttpNotFound();
        }
        ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", orders.CustomerId);
        return View(orders);
    }
    //For Admin
    // POST: Orders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "OrderId,CustomerId,TotalAmount,OrderDate,OrderStatus,SpicialInstruction")] Orders orders)
    {
        if (ModelState.IsValid)
        {
            db.Entry(orders).State = EntityState.Modified;

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", orders.CustomerId);
        return View(orders);
    }



    // GET: Orders/Delete/5
    public async Task<ActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Orders orders = await db.Orders.FindAsync(id);
        if (orders == null)
        {
            return HttpNotFound();
        }
        return View(orders);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        Orders orders = await db.Orders.FindAsync(id);
        db.Orders.Remove(orders);
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
}
