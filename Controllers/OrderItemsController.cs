using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class OrderItemsController : Controller
    {
        private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();

        // GET: OrderItems
        public ActionResult Index()
        {
            return View();
        }

        //For Customer
        // Get OrderItems/Details/1
        // Get OrderItems/Details/OrderId
        public ActionResult Details(int OrderId)
        {
            Orders orders = db.Orders.Find(OrderId);
            //OrderItems orderItem = db.OrderItems.Find(OrderId);
            List<OrderItems> orderItems = db.OrderItems.Where(oI => oI.OrderId == OrderId).ToList();
            return View(orderItems);

        }


        //For Admin

        // Get OrderItems/AdminOrderDetails/1
        // Get OrderItems/AdminOrderDetails/OrderId
        public ActionResult AdminOrderDetails(int OrderId)
        {
            Orders orders = db.Orders.Find(OrderId);
            //OrderItems orderItem = db.OrderItems.Find(OrderId);
            List<OrderItems> AdminOrderItems = db.OrderItems.Where(oI => oI.OrderId == OrderId).ToList();
            ViewBag.OrderId = OrderId;
            return View(AdminOrderItems);
            
        }

        //For Admin 
        // Get OrderItems/Edit/2
        // Get OrderItems/Edit/ItemId
        public ActionResult Edit(int OrderId)
        {
            OrderItems orderItems = db.OrderItems.FirstOrDefault(o => o.OrderId == OrderId);
            return View(orderItems);
        }


        //For Admin 
        // Get OrderItems/Edit
        // Get OrderItems/Edit


        [HttpPost]
        public ActionResult Edit([Bind(Include = "OrderId,Quantity,ItemId,ItemStatus")] OrderItems orderItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItems).State = EntityState.Modified;


                
                db.SaveChanges();

                return RedirectToAction("Index", "Orders");
            }

            return View("Index", "MenuList");
        }


    }
}