using FourYears.Areas.BookStoreAreas.Models;
using FourYears.Areas.BookStoreAreas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.BookStoreAreas.Controllers
{
    public class AccountController : Controller
    {
        // GET: BookStoreAreas/Account
        private IRepository_BookStoreSystemModel<Customer> db_Customer = new Repository_BookStoreSystemModel<Customer>();
        private IRepository_BookStoreSystemModel<Publisher> db_Publisher = new Repository_BookStoreSystemModel<Publisher>();
        private IRepository_BookStoreSystemModel<BookStoreAdmin> db_BookStoreAdmin = new Repository_BookStoreSystemModel<BookStoreAdmin>();


        [HttpGet]
        public ActionResult AdminLogin()
        {
            Response.Cookies["PublisherUserName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherID"].Expires = DateTime.Now.AddSeconds(-1);

            Response.Cookies["Account"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["FullName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["NickName"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(ViewModel_AdminLogin vm_AdminLogin)
        {
            if (ModelState.IsValid)
            {
                var LoginAdmin = db_BookStoreAdmin.GetAll().FirstOrDefault(bookStoreAdmin => bookStoreAdmin.AdminName == vm_AdminLogin.AdminName && bookStoreAdmin.AdminPassword == bookStoreAdmin.AdminPassword);
                if  (LoginAdmin != null)
                {
                    Response.Cookies["AdminName"].Value = LoginAdmin.AdminName;
                    Response.Cookies["AdminID"].Value = LoginAdmin.AdminID.ToString();
                    return RedirectToAction("AdminPage", "Admin", new { Area = "BookStoreAreas" });
                }
            }
            return View();
        }

        //一般顧客
        [HttpGet]
        public ActionResult CustomerRegister()    //Get方法→顧客註冊
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerRegister(Customer customer)   //Post方法→顧客註冊(驗證)
        {
            if (ModelState.IsValid)
            {
                db_Customer.Create(customer);
                return RedirectToAction("CustomerLogin", "Account", new { Area = "BookStoreAreas" });
            }
            return View();
        }

        [HttpGet]
        public ActionResult CustomerLogin()   //Get方法→顧客登入
        {
            Response.Cookies["Account"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["FullName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["NickName"].Expires = DateTime.Now.AddSeconds(-1);

            Response.Cookies["PublisherUserName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherID"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult CustomerLogin(ViewModel_CustomerLogin vm_CustomerLogin)   //Post方法→顧客登入，抓Cookies
        {
            if (ModelState.IsValid)
            {
                var LoginUser = db_Customer.GetAll().FirstOrDefault(customer => customer.Account == vm_CustomerLogin.Account && customer.Password == vm_CustomerLogin.Password);
                if (LoginUser != null)
                {
                    Response.Cookies["Account"].Value = LoginUser.Account;
                    Response.Cookies["FullName"].Value = HttpUtility.UrlEncode(LoginUser.FullName);
                    Response.Cookies["CustomerID"].Value = LoginUser.CustomerID.ToString();
                    Response.Cookies["NickName"].Value = HttpUtility.UrlEncode(LoginUser.NickName);
                    if (vm_CustomerLogin.RememberMe)
                    {
                        Response.Cookies["Account"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["FullName"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["CustomerID"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["NickName"].Expires = DateTime.Now.AddDays(3);

                    }
                    return RedirectToAction("CustomerProfile", "Customer", new { Area = "BookStoreAreas" });
                }
            }
            ViewBag.error = "帳號或密碼錯誤";
            return View();
        }

        public ActionResult Logout()   //顧客and廠商登出，清除Cookies
        {
            Response.Cookies["Account"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["FullName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["NickName"].Expires = DateTime.Now.AddSeconds(-1);

            Response.Cookies["PublisherUserName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherID"].Expires = DateTime.Now.AddSeconds(-1);

            Response.Cookies["AdminName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["AdminID"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return RedirectToAction("Browse", "Customer", new { Area = "BookStoreAreas" });
        }

        //*********************    廠商(出版社)    *************************//

        [HttpGet]
        public ActionResult PublisherRegister()    //Get方法→廠商註冊
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublisherRegister(Publisher publisher)   //Post方法→廠商註冊(驗證)
        {
            if (ModelState.IsValid)
            {
                db_Publisher.Create(publisher);
                return RedirectToAction("PublisherLogin", "Account", new { Area = "BookStoreAreas" });
            }
            return View();
        }

        [HttpGet]
        public ActionResult PublisherLogin()   //Get方法→廠商登入
        {
            Response.Cookies["PublisherUserName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["PublisherID"].Expires = DateTime.Now.AddSeconds(-1);

            Response.Cookies["Account"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["FullName"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies["NickName"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult PublisherLogin(ViewModel_PublisherLogin vm_PublisherLogin)   //Post方法→廠商登入，抓Cookies
        {
            if (ModelState.IsValid)
            {
                var LoginPublisher = db_Publisher.GetAll().FirstOrDefault(publisher => publisher.PublisherUserName == vm_PublisherLogin.PublisherUserName && publisher.PublisherPassword == vm_PublisherLogin.PublisherPassword);
                if (LoginPublisher != null)
                {
                    Response.Cookies["PublisherUserName"].Value = LoginPublisher.PublisherUserName;
                    Response.Cookies["PublisherName"].Value = HttpUtility.UrlEncode(LoginPublisher.PublisherName);
                    Response.Cookies["PublisherID"].Value = LoginPublisher.PublisherID.ToString();
                    if (vm_PublisherLogin.RememberMe)
                    {
                        Response.Cookies["PublisherUserName"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["PublisherName"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["PublisherID"].Expires = DateTime.Now.AddDays(3);
                    }
                    return RedirectToAction("PublisherProfile", "BookStore", new { Area = "BookStoreAreas" });
                }
            }
            ViewBag.error = "帳號或密碼錯誤";
            return View();
        }
    }
}