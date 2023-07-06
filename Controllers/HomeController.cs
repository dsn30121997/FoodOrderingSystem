using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;


namespace FoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        private FoodOrderingSystemDbContext db = new FoodOrderingSystemDbContext();

        // GET: Contact Page(return Contact page)
        public ActionResult Contact()
        {
        
            return View();
        }

        // GET: Home(return login page)
        public ActionResult Login()
        {
            Customer login = new Customer();
            return View(login);
        }
        
        //checking email and password for login Customer

        [HttpPost]
        public ActionResult Login(Customer model) 
        {
            Customer login = db.Customer.Where(l => l.Email == model.Email && l.Password == model.Password).FirstOrDefault();
               if(login!=null)
               {
                    Session.Add("Id", login.CustomerId);
                    Session.Add("Name", login.CustomerName);
                    Session.Add("SignInTime", DateTime.Now);
                    return RedirectToAction("MenuListCustomer", "Home");
               }
            else 
            {
                ViewBag.Message = "Error occured - Login Credentials are invalid !!";
                return View("Error");
            }
        }

        //Get MenuList for Customer after login
        public async Task<ActionResult> MenuListCustomer()
        {
            return View(await db.MenuList.ToListAsync());
        }

        //Get Admin Page
        public ActionResult AdminLogin()
        {
            Admin Adminlogin = new Admin();
            return View(Adminlogin);
        }

        //checking email and password for Admin login 

        [HttpPost]
        public ActionResult AdminLogin(Admin model)
        {
            Admin login = db.Admin.Where(l => l.Email == model.Email && l.Password == model.Password).FirstOrDefault();
            if (login != null)
            {
                Session.Add("Id", login.AdminId);
                Session.Add("Name", login.Name);
                Session.Add("SignInTime", DateTime.Now);
                return RedirectToAction("Index", "MenuLists");
            }
            else
            {
                ViewBag.Message = "Error occured - Login Credentials are invalid !!";
                return View("Error");
            }
        }


        
        //For Employee

        // GET: Home(return login page)
        public ActionResult EmployeeLogin()
        {
            Employee login = new Employee();
            return View(login);
        }
        //For Employee
        //checking email and password for login Employee

        [HttpPost]
        public ActionResult EmployeeLogin(Employee model)
        {
            Employee login = db.Employee.Where(l => l.Email == model.Email && l.Password == model.Password).FirstOrDefault();
            if (login != null)
            {
                Session.Add("Id", login.EmployeeId);
                Session.Add("Name", login.EmployeeName);
                Session.Add("SignInTime", DateTime.Now);
                return RedirectToAction("EmployeeOrdersIndex", "Orders");
            }
            else
            {
                ViewBag.Message = "Error occured - Login Credentials are invalid !!";
                return View("Error");
            }
        }



        //logout functionality
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login","Home");
        }




    }
}