using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
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


        public ActionResult Details(int OrderId)
        {
            Orders orders = db.Orders.Find(OrderId);
            //OrderItems orderItem = db.OrderItems.Find(OrderId);
            List<OrderItems> orderItems = db.OrderItems.Where(oI => oI.OrderId == OrderId).ToList();
            return View(orderItems);

        }

        





    }
}