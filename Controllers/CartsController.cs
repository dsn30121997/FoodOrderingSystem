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
using Antlr.Runtime;
using Microsoft.Ajax.Utilities;

namespace FoodOrderingSystem.Controllers
{
    public class CartsController : Controller
    {
        private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();

        public ActionResult AddToCart(int id)
        {
            MenuList ml =  db.MenuList.Find(id);
            Cart obj = new Cart();
            obj.CustomerId = (int)Session["Id"];
            obj.ItemId = id;
            obj.Quantity = 1;

            obj.TotalAmount = ml.Price * obj.Quantity;
            db.Cart.Add(obj);
            db.SaveChanges();
            return RedirectToAction("MenuListCustomer","Home");
        }


        //[HttpPost]
        //public async Task<ActionResult> AddToCart(int? CustomerId,int? ItemId,int Quantity, decimal TotalAmount, Cart cart)
        //{
            
        //    MenuList menuList = new MenuList();

        //    if (Session["Id"] != null)
        //    {
        //        ViewBag.CustomerId = Session["Id"];

        //        //Customer customer = await db.Customer.FindAsync(CustomerId);
        //        MenuList MenuList = await db.MenuList.FindAsync(ItemId);
               
        //        db.Cart.Add(cart);
        //        await db.SaveChangesAsync();
                
        //        return RedirectToAction("CartDetails",CustomerId);
        //    }

        //    //ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", cart.CustomerId);
        //    //ViewBag.ItemId = new SelectList(db.MenuList, "ItemId", "ItemName", cart.ItemId);
        //    //ViewBag.ItemId =  new SelectListItem { Text = "ItemId", Value=ItemId};
        //    return View(cart);
        //}


        //public async Task<ActionResult> CartDetails()
        //{

        //    var cart = db.Cart.Include(c => c.Customer).Include(c => c.MenuList);
        //    return View(await db.Cart.ToListAsync());
        //}


        //public ActionResult CartDetails(int CustomerId)
        //{
        //    Customer customer = new Customer();

        //    // return View(db.Cart.ToListAsync());
        //    Cart cart = db.Cart.Find(customer.CustomerId);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("CartDetails");
        //}


        // Get CartDetails/2
        // Get CartDetails/CustomerId

        public ActionResult CartDetails(int? id)
        {
            Customer customer = db.Customer.Find(id);
           
            if (customer == null)
            {
                return HttpNotFound();
            }

            List<Cart> carts = db.Cart.Where(c => c.CustomerId == id).ToList();
            return View(carts);
        }
            
        public async Task<ActionResult> UpdateCart(int id)
        {
            Cart cart = new Cart();
            cart = await db.Cart.FindAsync(cart.ItemId);

            db.Entry(cart).State = EntityState.Modified;
            
            await db.SaveChangesAsync();

            if (id == null && cart == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(cart);
        }


        // POST: Cart/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCart([Bind(Include = "CustomerId,ItemId,Id,Quantity,TotalAmount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CartDetails");
            }
            return View("CartDetails");
        }



        // GET: Carts/Delete/5
        public  ActionResult Delete(int? CustomerId)
        {


            Customer customer = db.Customer.Find(CustomerId);

            if (customer == null)
            {
                return HttpNotFound();
            }

            List<Cart> carts = db.Cart.Where(c => c.CustomerId == CustomerId).ToList();
            return View("CartDetails");

            
        }











































        // GET: Carts
        public async Task<ActionResult> Index()
        {
            var cart = db.Cart.Include(c => c.Customer).Include(c => c.MenuList);
            return View(await cart.ToListAsync());
        }

        // GET: Carts/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = await db.Cart.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            ViewBag.ItemId = new SelectList(db.MenuList, "ItemId", "ItemName");



            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerId,ItemId,Id,Quantity,TotalAmount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Cart.Add(cart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = Session["Id"];//new SelectList(db.Customer, "CustomerId", "CustomerName", cart.CustomerId);
            ViewBag.ItemId = new SelectList(db.MenuList, "ItemId", "ItemName", cart.ItemId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<ActionResult> Edit(int? CustomerId,int? ItemId)
        {
            if (CustomerId == null && ItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = await db.Cart.FindAsync(CustomerId);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", cart.CustomerId);
            ViewBag.ItemId = new SelectList(db.MenuList, "ItemId", "ItemName", cart.ItemId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerId,ItemId,Id,Quantity,TotalAmount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", cart.CustomerId);
            ViewBag.ItemId = new SelectList(db.MenuList, "ItemId", "ItemName", cart.ItemId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = await db.Cart.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cart cart = await db.Cart.FindAsync(id);
            db.Cart.Remove(cart);
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
