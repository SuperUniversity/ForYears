using FourYears.Areas.JobArea.Models;
using FourYears.Areas.JobArea.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.JobArea.Controllers
{
    public class EmployerController : Controller
    {
        // GET: JobArea/Employer
        private IRepository<EmployerCompany> db = new Repository<EmployerCompany>();
        private superuniversityEntities1 su = new superuniversityEntities1();
        // GET: JobArea/Employer
        public ActionResult Index()
        {
            List<EmployerCompany> Emp = db.GetAll().ToList();
            ViewBag.Result = TempData["Result"];
            return View(Emp);
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(EmployerCompany emp)
        {
            if (this.ModelState.IsValid)
            {
                db.Create(emp);
                TempData["Result"] = String.Format("雇主{0}新增成功", emp.CompanyName);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Result = "資料錯誤，請檢查";
                return View(emp);
            }

        }
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {

            EmployerCompany _EPC = db.GetById(id);
            return View(_EPC);

        }
        [HttpPost]
        public ActionResult Edit(EmployerCompany emp, int id)
        {
            emp.CompanyID = id;
            db.Update(emp);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            db.Delete(db.GetById(id));
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(EmployerLogin login, Job j)
        {

            var Employer = su.EmployerCompany.FirstOrDefault(u => u.EmployerMail == login.EmployerMail && u.Password == login.Password);
            var User = su.User.FirstOrDefault(u => u.UserEmail == login.EmployerMail && u.Password == login.Password);
            if (Employer != null)
            {
                Response.Cookies["name"].Value = HttpUtility.UrlEncode(Employer.CompanyName);
                Response.Cookies["nameid"].Value = Employer.CompanyID.ToString();
                if (login.RememberMe)
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(7);
                }
                return RedirectToAction("JobManage", "Manager");
                //int.Parse(Request.Cookies["nameID"].Value)
            }
            else if (User != null)
            {
                Response.Cookies["name"].Value = HttpUtility.UrlEncode(User.UserName);
                Response.Cookies["nameid"].Value = (-User.UserID).ToString();
                if (login.RememberMe)
                {
                    Response.Cookies["name"].Expires = DateTime.Now.AddDays(7);
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.error = "帳號或密碼錯誤";
            return View();


        }
        public ActionResult Logout()
        {
            Response.Cookies["name"].Expires = DateTime.Now.AddSeconds(-1);
            Session.Abandon();
            return RedirectToAction("Index", "HomePage");
        }
    }
}