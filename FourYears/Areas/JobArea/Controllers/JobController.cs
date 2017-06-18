using FourYears.Areas.JobArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.JobArea.Controllers
{
    public class JobController : Controller
    {
        private IRepository<Job> db = new Repository<Job>();
        private superuniversityEntities1 su = new superuniversityEntities1();
        // GET: JobArea/Job
        public ActionResult Index()
        {
            List<Job> jb = db.GetAll().ToList();
            ViewBag.Result = TempData["Result"];
            return View(jb);
        }
        [HttpGet]
        public ActionResult Insert(int id)
        {

            ViewBag.id = id;
            ViewBag.datas = su.Jobtime.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Job j, HttpPostedFileBase byteimg)
        {
            j.CompanyID = int.Parse(Request.Cookies["nameID"].Value);
            ViewBag.datas = su.Jobtime.ToList();
            if (ModelState.IsValid)
            {
                if (byteimg != null)
                {

                    j.Image = new byte[byteimg.ContentLength];
                    byteimg.InputStream.Read(j.Image, 0, byteimg.ContentLength);
                }
                db.Create(j);
                TempData["Result"] = string.Format("工作{0}新增成功", j.JobName);

                return RedirectToAction("Index");
            }
            ViewBag.Result = "資料錯誤，請檢查";
            return View(j);

        }
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.datas = su.Jobtime.ToList();
            return View(su.Job.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Job j, int id, HttpPostedFileBase byteimg)
        {
            if (byteimg != null)
            {
                j.Image = new byte[byteimg.ContentLength];
                byteimg.InputStream.Read(j.Image, 0, byteimg.ContentLength);
            }
            j.JobID = id;
            db.Update(j);
            ViewBag.datas = su.Jobtime.ToList();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            db.Delete(db.GetById(id));
            return RedirectToAction("Index");

        }
    }
}