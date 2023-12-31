﻿using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class PaymentsController : Controller
    {
       private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();


        //For Admin
        public async Task<ActionResult> Index()
        {
            
            return View(await db.Payment.ToListAsync());
        }



       // Getting Payment Page
       public ActionResult Create(int OrderId)
        {
           Orders orders = db.Orders.Find(OrderId);

            Payment payment = new Payment();
            payment.OrderId = orders.OrderId;
            payment.TotalAmount = orders.TotalAmount;

            return View("Create",payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,OrderId,TotalAmount,PaymentMethod,PaymentDate")]Payment payment)
        {
            
            payment.PaymentDate = DateTime.Now;
            int b = payment.OrderId;
            decimal a= payment.TotalAmount;

            db.Payment.Add(payment);
            db.SaveChanges();


            

            return View("Success");
        }


    }
}