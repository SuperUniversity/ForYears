using FourYears.Areas.FoodMapArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.FoodMapArea.Controllers
{
    public class ShopController : Controller
    {
        private superuniversityEntities db = new superuniversityEntities();
        // GET: FoodMapArea/Shop

        public ActionResult Index(int id = 1)
        {
            ViewBag.Message = string.Format("搜尋結果");
            var shop = db.Shop.Where(p => p.FoodCategoryID == id);
            return View(shop.ToList());
        }
        public ActionResult Details(int id = 0)
        {
            ViewBag.number = id;
            if (db.Shop.Find(id) != null)
            {
                ViewBag.image = db.Shop.Find(id).Image1;
                ViewBag.image2 = db.Shop.Find(id).Image2;
                ViewBag.image3 = db.Shop.Find(id).Image3;
                ViewBag.fc = db.Shop.Find(id).FoodCategoryID;
            }
            return View(db.Shop.Find(id));
        }

        public ActionResult GetImage1(int id)
        {
            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage1;

            return File(img, "image/jpeg");
        }

        public ActionResult GetImage2(int id)
        {
            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage2;

            return File(img, "image/jpeg");
        }

        public ActionResult GetImage3(int id)
        {
            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage3;

            return File(img, "image/jpeg");
        }

        public ActionResult GetFoodCategoryName()
        {


            return PartialView(db.FoodCategory.ToList());
        }


    }
}