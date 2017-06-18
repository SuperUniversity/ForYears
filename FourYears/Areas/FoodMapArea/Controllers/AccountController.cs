using FourYears.Areas.FoodMapArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.FoodMapArea.Controllers
{
    public class AccountController : Controller
    {
        private superuniversityEntities db = new superuniversityEntities();
        // GET: FoodMapArea/Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ShopCustomer shopcustomer)
        {
            if (ModelState.IsValid)
            {
                db.ShopCustomer.Add(shopcustomer);
                db.SaveChanges();
                return RedirectToAction("Login", "Account", new { Area = "FoodMapArea" });
            }


            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ShopCustomer shopcustomer)
        {
            if (ModelState.IsValid)
            {
                var loginUser = db.ShopCustomer.FirstOrDefault(u => u.EmailAddress == shopcustomer.EmailAddress && u.Password == shopcustomer.Password);
                if (loginUser != null)
                {
                    Response.Cookies["UserName"].Value = loginUser.FullName;
                    Response.Cookies["CustomerID"].Value = loginUser.CustomerID.ToString();
                    return RedirectToAction("Index", "Customer", new { Area = "FoodMapArea" });
                }
            }


            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return RedirectToAction("Index", "Customer", new { Area = "FoodMapArea" });
        }
    }
}