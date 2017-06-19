using FourYears.Areas.FoodMapArea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;

namespace FourYears.Areas.FoodMapArea.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private superuniversityEntities2 db = new superuniversityEntities2();
        // GET: FoodMapArea/Admin
       
        public ActionResult Index(int? page)
        {
            return View(db.Shop.ToList().ToPagedList(page ?? 1, 10));
        }

        public ActionResult GetImage(int id = 1)
        {
            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage1;
            return File(img, "image/jpeg");
        }

        public ActionResult GetImage2(int id = 1)
        {

            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage2;
            return File(img, "image/jpeg");

        }

        public ActionResult GetImage3(int id = 1)
        {
            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage3;
            return File(img, "image/jpeg");

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.datas = db.FoodCategory.ToList();
            ViewBag.cities = db.City.ToList();
            ViewBag.schools = db.School.ToList();
            //var school = from p in db.School
            //             where p.CityID == cityid
            //             join d in db.City
            //             on p.CityID  equals  d.CityID 
            //             select  p;
            return View();
        }

        public ActionResult PartialDrop2()
        {
            ViewBag.categories = db.FoodCategory.ToList();

            SelectList selectList = new SelectList(this.GetCity(), "CityID", "CityName");

            ViewBag.cities = selectList;

            ViewBag.schools = db.School.ToList();

            return PartialView();
        }

        private IEnumerable<City> GetCity()
        {
            using (superuniversityEntities2 db = new superuniversityEntities2())
            {
                var query = db.City.OrderBy(x => x.CityID);
                return query.ToList();
            }
        }

        [HttpPost]
        public JsonResult Schools(int CityID)
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

            if (CityID > 0)
            {
                var schools = this.GetSchool(CityID);
                if (schools.Count() > 0)
                {
                    foreach (var school in schools)
                    {
                        items.Add(new KeyValuePair<string, string>(
                            school.SchoolID.ToString(),
                            school.SchoolName.ToString()));
                    }
                }
            }
            return this.Json(items);
        }

        private IEnumerable<School> GetSchool(int CityID)
        {
            using (superuniversityEntities2 db = new superuniversityEntities2())
            {
                var query = db.School.Where(x => x.CityID == CityID);
                return query.ToList();
            }
        }


        [HttpPost]
        public ActionResult Create(Shop shop, Shop_ShopCustomer shop_customer, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {
            if (Image1 != null)
            {
                string strPath1 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image1.FileName;
                Image1.SaveAs(strPath1);
                var imgSize1 = Image1.ContentLength;
                byte[] imgByte1 = new byte[imgSize1];
                Image1.InputStream.Read(imgByte1, 0, imgSize1);
                shop.Image1 = Image1.FileName;
                shop.BytesImage1 = imgByte1;


                if (Image2 != null)
                {
                    string strPath2 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image2.FileName;
                    Image2.SaveAs(strPath2);
                    var imgSize2 = Image2.ContentLength;
                    byte[] imgByte2 = new byte[imgSize2];
                    Image2.InputStream.Read(imgByte2, 0, imgSize2);
                    shop.Image2 = Image2.FileName;
                    shop.BytesImage2 = imgByte2;
                }


                if (Image3 != null)
                {
                    string strPath3 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image3.FileName;
                    Image3.SaveAs(strPath3);
                    var imgSize3 = Image3.ContentLength;
                    byte[] imgByte3 = new byte[imgSize3];
                    Image3.InputStream.Read(imgByte3, 0, imgSize3);
                    shop.Image3 = Image3.FileName;
                    shop.BytesImage3 = imgByte3;
                }

                //shop_customer.CustomerID = Convert.ToInt32(Request.Cookies["CustomerID"].Value);
                shop_customer.CustomerID = User.Identity.GetUserId();

                db.Shop_ShopCustomer.Add(shop_customer);
                db.Shop.Add(shop);
                db.SaveChanges();

                return RedirectToAction("Index", "Admin", new { Area = "FoodMapArea" });
            }
            ViewBag.Message = "請至少選擇一張圖片";
            ViewBag.datas = db.FoodCategory.ToList();
            ViewBag.cities = db.City.ToList();
            ViewBag.schools = db.School.ToList();
            return View();

        }

        public ActionResult Detail(int id = 0)
        {
            ViewBag.datas = db.FoodCategory.ToList();
            ViewBag.cities = db.City.ToList();
            ViewBag.schools = db.School.ToList();
            ViewBag.number = id;
            ViewBag.image2 = db.Shop.Find(id).Image2;
            ViewBag.image3 = db.Shop.Find(id).Image3;
            return View(db.Shop.Find(id));
        }

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            ViewBag.datas = db.FoodCategory.ToList();
            ViewBag.cities = db.City.ToList();
            ViewBag.schools = db.School.ToList();
            return View(db.Shop.Find(id));
        }



        [HttpPost]
        public ActionResult Update(Shop shop, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {

            if (Image1 != null)
            {
                string strPath1 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image1.FileName;
                Image1.SaveAs(strPath1);
                var imgSize1 = Image1.ContentLength;
                byte[] imgByte1 = new byte[imgSize1];
                Image1.InputStream.Read(imgByte1, 0, imgSize1);
                shop.Image1 = Image1.FileName;
                shop.BytesImage1 = imgByte1;


                if (Image2 != null)
                {
                    string strPath2 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image2.FileName;
                    Image2.SaveAs(strPath2);
                    var imgSize2 = Image2.ContentLength;
                    byte[] imgByte2 = new byte[imgSize2];
                    Image2.InputStream.Read(imgByte2, 0, imgSize2);
                    shop.Image2 = Image2.FileName;
                    shop.BytesImage2 = imgByte2;
                }


                if (Image3 != null)
                {
                    string strPath3 = Request.PhysicalApplicationPath + @"Areas/FoodMapArea/ShopImages/" + Image3.FileName;
                    Image3.SaveAs(strPath3);
                    var imgSize3 = Image3.ContentLength;
                    byte[] imgByte3 = new byte[imgSize3];
                    Image3.InputStream.Read(imgByte3, 0, imgSize3);
                    shop.Image3 = Image3.FileName;
                    shop.BytesImage3 = imgByte3;
                }



                db.Entry(shop).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Admin", new { Area = "FoodMapArea" });
            }
            ViewBag.Message = "請至少選擇一張圖片";
            ViewBag.datas = db.FoodCategory.ToList();
            ViewBag.cities = db.City.ToList();
            ViewBag.schools = db.School.ToList();
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            DialogResult result = MessageBox.Show("是否確定刪除此筆資料?", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                db.Shop.Remove(db.Shop.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index", "Admin", new { Area = "FoodMapArea" });
            }
            return RedirectToAction("Index", "Admin", new { Area = "FoodMapArea" });
        }
    }
}