using FourYears.Areas.JobArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.JobArea.Controllers
{
    public class ManagerController : Controller
    {
        private IRepository<EmployerCompany> db = new Repository<EmployerCompany>();
        private IRepository<Job> jb = new Repository<Job>();
        private superuniversityEntities1 su = new superuniversityEntities1();
        // GET: JobArea/Manager
        public ActionResult Index(int id)
        {
            return View(db.GetById(id));
        }
        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            EmployerCompany emp = db.GetById(id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult ChangePassword(EmployerCompany emp, int id)
        {
            emp.CompanyID = id;
            db.Update(emp);
            return RedirectToAction("Index", new { id = Request.Cookies["nameid"].Value });
        }
        public ActionResult JobManage()
        {

            var result = (from s in su.Job.AsEnumerable()
                          where (s.CompanyID == int.Parse(Request.Cookies["nameid"].Value))
                          select s);
            var list = result;
            return View(list.ToList());
        }

        public ActionResult JobEdit(int id)
        {
            ViewBag.datas = su.Jobtime.ToList();
            return View(su.Job.Find(id));
        }
        [HttpPost]
        public ActionResult JobEdit(Job j, int id, HttpPostedFileBase byteimg)
        {
            if (byteimg != null)
            {
                j.Image = new byte[byteimg.ContentLength];
                byteimg.InputStream.Read(j.Image, 0, byteimg.ContentLength);
            }
            j.JobID = id;
            jb.Update(j);
            ViewBag.datas = su.Jobtime.ToList();
            return RedirectToAction("Index", new { id = Request.Cookies["nameid"].Value });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployerCompany _EPC = db.GetById(id);
            return View(_EPC);
        }
        [HttpPost]
        public ActionResult Edit(EmployerCompany emp, int id)
        {
            emp.CompanyID = id;
            db.Update(emp);
            return RedirectToAction("Index", new { id = id });
        }
    }
}