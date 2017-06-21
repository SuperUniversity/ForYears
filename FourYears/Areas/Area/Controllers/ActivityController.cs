using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FourYears.Areas.Area.Models;

namespace FourYears.Areas.Area.Controllers
{
    public class ActivityController : Controller
    {
        
        public ActionResult Index()
        {
            List<Models.Activity> Activites = ActivityDataContext.LoadActivites();
            return View(Activites);
        }
        public ActionResult Insert()
        {
            if (Request.Form.Count > 0)
            {
                Models.Activity _Activity = new Models.Activity();
                _Activity.ActivityName = Request.Form["ActivityName"];
                _Activity.Description = Request.Form["Description"];
                _Activity.Location = Request.Form["Location"];
                _Activity.Host = Request.Form["Host"];
                ActivityDataContext.InsertActivity(_Activity);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Activity _Activity = ActivityDataContext.LoadActivityByID(id);
            return View(_Activity);
        }

        [HttpPost]
        public ActionResult Edit()
        {
            Models.Activity _Activity = new Models.Activity();
            _Activity.ActivityID = Convert.ToInt32(Request.Form["ActivityID"]);
            _Activity.ActivityName = Request.Form["ActivityName"];
            _Activity.Description = Request.Form["Description"];
            _Activity.Location = Request.Form["Location"];
            _Activity.Host = Request.Form["Host"];
            ActivityDataContext.EditActivity(_Activity);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {

            ActivityDataContext.DeleteActivity(id);
            return RedirectToAction("Index");
        }

    }

}