using FourYears.Areas.FoodMapArea.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace FourYears.Areas.FoodMapArea.Controllers
{
    public class UserController : Controller
    {

        private superuniversityEntities db = new superuniversityEntities();
        // GET: FoodMapArea/Admin
        public ActionResult Index(int? page, int id = 0)
        {
            var result = from p in db.ShopCustomer
                         join s in db.Shop_ShopCustomer on p.CustomerID equals s.CustomerID
                         join c in db.Shop on s.ShopID equals c.ShopID
                         where p.CustomerID == id
                         select new UserTemp
                         {
                             ShopID = c.ShopID,
                             Address = c.Address,
                             BusinessTime = c.BusinessTime,
                             BytesImage1 = c.BytesImage1,
                             BytesImage2 = c.BytesImage2,
                             BytesImage3 = c.BytesImage3,
                             Phone = c.Phone,
                             Image1 = c.Image1,
                             Image2 = c.Image2,
                             Image3 = c.Image3,
                             Description = c.Description,
                             Cost = c.Cost,
                             ShopName = c.ShopName,
                             FoodCategoryID = c.FoodCategoryID,
                             ShopLink = c.ShopLink,
                             CityID = c.CityID,
                             SchoolID = c.SchoolID,
                             CustomerID = p.CustomerID
                         };

            return View(result.ToList().ToPagedList(page ?? 1, 10));
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

        public ActionResult PartialDrop3()
        {
            ViewBag.categories = db.FoodCategory.ToList();

            SelectList selectList = new SelectList(this.GetCity(), "CityID", "CityName");

            ViewBag.cities = selectList;

            ViewBag.schools = db.School.ToList();

            return PartialView();
        }

        private IEnumerable<City> GetCity()
        {
            using (superuniversityEntities db = new superuniversityEntities())
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
            using (superuniversityEntities db = new superuniversityEntities())
            {
                var query = db.School.Where(x => x.CityID == CityID);
                return query.ToList();
            }
        }


        [HttpPost]
        public ActionResult Create(Shop shop, Shop_ShopCustomer shop_shopcustomer, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
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

                shop_shopcustomer.CustomerID = Convert.ToInt32(Request.Cookies["CustomerID"].Value);
                db.Shop_ShopCustomer.Add(shop_shopcustomer);
                db.Shop.Add(shop);
                db.SaveChanges();

                return RedirectToAction("Index", "User", new { id = Request.Cookies["CustomerID"].Value, Area = "FoodMapArea" });
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

                return RedirectToAction("Index", "User", new { id = Request.Cookies["CustomerID"].Value, Area = "FoodMapArea" });
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
                return RedirectToAction("Index", "User", new { id = Request.Cookies["CustomerID"].Value, Area = "FoodMapArea" });
            }
            return RedirectToAction("Index", "User", new { id = Request.Cookies["CustomerID"].Value, Area = "FoodMapArea" });
        }
    }

    public class UserTemp
    {
        [DisplayName("餐廳編號")]
        public int ShopID { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("電話")]
        public string Phone { get; set; }
        [DisplayName("圖片1")]
        public string Image1 { get; set; }
        public byte[] BytesImage1 { get; set; }
        [DisplayName("介紹")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DisplayName("平均消費")]
        public string Cost { get; set; }
        [DisplayName("營業時間")]
        public string BusinessTime { get; set; }
        [DisplayName("餐廳名稱")]
        public string ShopName { get; set; }
        [DisplayName("分類編號")]
        public Nullable<int> FoodCategoryID { get; set; }
        [DisplayName("圖片2")]
        public string Image2 { get; set; }
        public byte[] BytesImage2 { get; set; }
        [DisplayName("圖片3")]
        public string Image3 { get; set; }
        public byte[] BytesImage3 { get; set; }
        [DisplayName("粉絲專頁")]
        public string ShopLink { get; set; }
        [DisplayName("縣市編號")]
        public Nullable<int> CityID { get; set; }
        [DisplayName("學校編號")]
        public Nullable<int> SchoolID { get; set; }

        [DisplayName("會員編號")]
        public int CustomerID { get; set; }
    }
}