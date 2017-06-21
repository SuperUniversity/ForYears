using FourYears.Areas.JobArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.JobArea.Controllers
{
    public class HomePageController : Controller
    {
        // GET: JobArea/HomePage
        superuniversityEntities3 db = new superuniversityEntities3();
        // GET: JobArea/HomePage
        public ActionResult Index(string Jobnamesreach = "", string Workplacesreach = "", string Allsreach = "")
        {
            var jobfront = new List<Job>();
            var list = db.Job.ToList();
            string allstr = string.IsNullOrEmpty(Allsreach) ? "" : Allsreach;
            string jobstr = string.IsNullOrEmpty(Jobnamesreach) ? "nothing" : Jobnamesreach;
            string workstr = string.IsNullOrEmpty(Workplacesreach) ? "nothing" : Workplacesreach;

            if (allstr == "" && jobstr == "nothing" && workstr == "nothing")
            {
                jobfront = list;
            }
            else if (allstr != "")
            {
                jobfront = list.Select(p => p).Where(p => p.JobName.Contains(allstr) || p.Workplace.Contains(allstr)).ToList();
            }
            else
            {
                jobfront = list.Select(p => p).Where(p => p.JobName.Contains(jobstr)).ToList()
                  .Union(list.Select(p => p).Where(p => p.Workplace.Contains(workstr))).ToList();
            }

            ViewBag.SreachbyName = jobstr;
            ViewBag.SreachbyPlace = workstr;
            ViewBag.SreachbyAll = allstr;
            return View(jobfront.ToList());
        }
    }
}