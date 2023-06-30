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

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

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

        // GET: Orders/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
        //    return View();
        //}

        // POST: Orders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int CustomerId,Orders orders)
        {
            if (Session["Id"] != null)
            {
                ViewBag.CustomerId = Session["Id"];
                
                Customer customer = await db.Customer.FindAsync(CustomerId);
                List<Cart> carts = db.Cart.Where(c => c.CustomerId == CustomerId).ToList();
                MenuList menuList = new MenuList();
                
                orders.CustomerId = customer.CustomerId;
                //orders.TotalAmount =;
                orders.OrderDate = DateTime.Now;
                orders.OrderStatus = "Recived Order";
                
                db.Orders.Add(orders);
                await db.SaveChangesAsync();




                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", orders.CustomerId);
            return View(orders);
        }

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
