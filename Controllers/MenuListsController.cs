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
    public class MenuListsController : Controller
    {
        private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();

        // GET: MenuLists
        public async Task<ActionResult> Index()
        {
            return View(await db.MenuList.ToListAsync());
        }

        // GET: MenuLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuList menuList = await db.MenuList.FindAsync(id);
            if (menuList == null)
            {
                return HttpNotFound();
            }
            return View(menuList);
        }


        //for Admin
        //getting Item Details
        // Get MenuList/AdminMenuDetails
        public async Task<ActionResult> AdminMenuDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuList menuList = await db.MenuList.FindAsync(id);
            if (menuList == null)
            {
                return HttpNotFound();
            }
            return View(menuList);
        }


        // GET: MenuLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemId,ItemName,Category,Subcategory,Description,Price")] MenuList menuList)
        {
            if (ModelState.IsValid)
            {
                db.MenuList.Add(menuList);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(menuList);
        }

        // GET: MenuLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuList menuList = await db.MenuList.FindAsync(id);
            if (menuList == null)
            {
                return HttpNotFound();
            }
            return View(menuList);
        }

        // POST: MenuLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemName,Category,Subcategory,Description,Price")] MenuList menuList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuList);
        }

        // GET: MenuLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuList menuList = await db.MenuList.FindAsync(id);
            if (menuList == null)
            {
                return HttpNotFound();
            }
            return View(menuList);
        }

        // POST: MenuLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MenuList menuList = await db.MenuList.FindAsync(id);
            db.MenuList.Remove(menuList);
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
