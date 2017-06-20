using FourYears.Areas.FoodMapArea.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.FoodMapArea.Controllers
{
    public class CustomerController : Controller
    {
        private superuniversityEntities2 db = new superuniversityEntities2();
        // GET: FoodMapArea/Customer


        public ActionResult Index()
        {
            Random r = new Random();
            ViewBag.num = r.Next(3, 8);
            return View(db.Shop.Find(ViewBag.num));
        }


        public ActionResult GetImage(int id = 1)
        {

            Shop shop = db.Shop.Find(id);
            byte[] img = shop.BytesImage1;
            return File(img, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Map(int? CityID = 1, int? SchoolID = 1, int? FoodCategoryID = 0, int t = 0)
        {
            if (FoodCategoryID == 0)
            {
                var result = from p in db.Shop
                             join s in db.School on p.SchoolID equals s.SchoolID
                             join f in db.FoodCategory on p.FoodCategoryID equals f.FoodCategoryID
                             where p.CityID == CityID && p.SchoolID == SchoolID
                             select new Temp
                             {
                                 ShopID = p.ShopID,
                                 Address = p.Address,
                                 Phone = p.Phone,
                                 Image1 = p.Image1,
                                 BytesImage1 = p.BytesImage1,
                                 Description = p.Description,
                                 Cost = p.Cost,
                                 BusinessTime = p.BusinessTime,
                                 ShopName = p.ShopName,
                                 FoodCategoryID = p.FoodCategoryID,
                                 Image2 = p.Image2,
                                 BytesImage2 = p.BytesImage2,
                                 Image3 = p.Image3,
                                 BytesImage3 = p.BytesImage3,
                                 ShopLink = p.ShopLink,
                                 CityID = p.CityID,
                                 SchoolID = p.SchoolID,
                                 SchoolName = s.SchoolName,
                                 FoodCategoryName = f.FoodCategoryName
                             };
                if (SchoolID == 15)
                {
                    ViewBag.check = 1;
                    ViewBag.name = "國立體育大學";
                    ViewBag.add1 = "桃園市龜山區大湖村文興路39號";
                    ViewBag.add2 = "桃園市龜山區文化一路226號";
                    ViewBag.add3 = "桃園市龜山區復興三路667號";
                    ViewBag.add4 = "桃園市龜山區大華村13鄰文化三路566號1樓";
                    ViewBag.add5 = "桃園市龜山區文化三路318號";
                    ViewBag.add6 = "桃園市龜山區復興一路212巷3號";
                    ViewBag.add7 = "桃園市龜山區復興一路292號";
                }


                ViewBag.categories = db.FoodCategory.ToList();
                ViewBag.cities = db.City.ToList();
                ViewBag.schools = db.School.ToList();
                return View(result.ToList());
                //db.Shop.ToList()
            }
            else
            {
                var result = from p in db.Shop
                             join s in db.School on p.SchoolID equals s.SchoolID
                             join f in db.FoodCategory on p.FoodCategoryID equals f.FoodCategoryID
                             where p.CityID == CityID && p.SchoolID == SchoolID && p.FoodCategoryID == FoodCategoryID
                             select new Temp
                             {
                                 ShopID = p.ShopID,
                                 Address = p.Address,
                                 Phone = p.Phone,
                                 Image1 = p.Image1,
                                 BytesImage1 = p.BytesImage1,
                                 Description = p.Description,
                                 Cost = p.Cost,
                                 BusinessTime = p.BusinessTime,
                                 ShopName = p.ShopName,
                                 FoodCategoryID = p.FoodCategoryID,
                                 Image2 = p.Image2,
                                 BytesImage2 = p.BytesImage2,
                                 Image3 = p.Image3,
                                 BytesImage3 = p.BytesImage3,
                                 ShopLink = p.ShopLink,
                                 CityID = p.CityID,
                                 SchoolID = p.SchoolID,
                                 SchoolName = s.SchoolName,
                                 FoodCategoryName = f.FoodCategoryName
                             };
                if (SchoolID == 15)
                {
                    ViewBag.check = 1;
                    ViewBag.name = "國立體育大學";
                    ViewBag.add1 = "桃園市龜山區大湖村文興路39號";
                    ViewBag.add2 = "桃園市龜山區文化一路226號";
                    ViewBag.add3 = "桃園市龜山區復興三路667號";
                    ViewBag.add4 = "桃園市龜山區大華村13鄰文化三路566號1樓";
                    ViewBag.add5 = "桃園市龜山區文化三路318號";
                    ViewBag.add6 = "桃園市龜山區復興一路212巷3號";
                    ViewBag.add7 = "桃園市龜山區復興一路292號";
                }


                ViewBag.categories = db.FoodCategory.ToList();
                ViewBag.cities = db.City.ToList();
                ViewBag.schools = db.School.ToList();
                return View(result.ToList());
                //db.Shop.ToList()
            }


        }


        [HttpPost]
        public ActionResult Map(int? CityID = 1, int? SchoolID = 1, int? FoodCategoryID = 0)
        {
            //var result = from p in db.Shop
            //             where p.CityID == CityID && p.SchoolID == SchoolID && p.FoodCategoryID == FoodCategoryID
            //             select p;
            if (FoodCategoryID == 0)
            {
                var result = from p in db.Shop
                             join s in db.School on p.SchoolID equals s.SchoolID
                             join f in db.FoodCategory on p.FoodCategoryID equals f.FoodCategoryID
                             where p.CityID == CityID && p.SchoolID == SchoolID
                             select new Temp
                             {
                                 ShopID = p.ShopID,
                                 Address = p.Address,
                                 Phone = p.Phone,
                                 Image1 = p.Image1,
                                 BytesImage1 = p.BytesImage1,
                                 Description = p.Description,
                                 Cost = p.Cost,
                                 BusinessTime = p.BusinessTime,
                                 ShopName = p.ShopName,
                                 FoodCategoryID = p.FoodCategoryID,
                                 Image2 = p.Image2,
                                 BytesImage2 = p.BytesImage2,
                                 Image3 = p.Image3,
                                 BytesImage3 = p.BytesImage3,
                                 ShopLink = p.ShopLink,
                                 CityID = p.CityID,
                                 SchoolID = p.SchoolID,
                                 SchoolName = s.SchoolName,
                                 FoodCategoryName = f.FoodCategoryName
                             };
                if (SchoolID == 1)
                {
                    ViewBag.name = "國立台灣大學";
                    ViewBag.add1 = "台北市大安區忠孝東路四段235號2樓";
                    ViewBag.add2 = "台北市大安區安和路二段217巷2弄3號2樓";
                    ViewBag.add3 = "台北市大安區光復南路260巷30號";
                    ViewBag.add4 = "台北市大安區羅斯福路4段24巷11號";
                    ViewBag.add5 = "台北市中正區羅斯福路三段100之1號";
                    ViewBag.add6 = "台北市大安區青田街7巷6號";
                    ViewBag.add7 = "台北市大安區四維路208巷18號之1";
                }
                else if (SchoolID == 6)
                {
                    ViewBag.name = "國立台灣藝術大學";
                    ViewBag.add1 = "新北市中和區立德街56號";
                    ViewBag.add2 = "新北市板橋區民權路3號";
                }
                return View(result.ToList());
            }
            else
            {
                var result = from p in db.Shop
                             join s in db.School on p.SchoolID equals s.SchoolID
                             join f in db.FoodCategory on p.FoodCategoryID equals f.FoodCategoryID
                             where p.CityID == CityID && p.SchoolID == SchoolID && p.FoodCategoryID == FoodCategoryID
                             select new Temp
                             {
                                 ShopID = p.ShopID,
                                 Address = p.Address,
                                 Phone = p.Phone,
                                 Image1 = p.Image1,
                                 BytesImage1 = p.BytesImage1,
                                 Description = p.Description,
                                 Cost = p.Cost,
                                 BusinessTime = p.BusinessTime,
                                 ShopName = p.ShopName,
                                 FoodCategoryID = p.FoodCategoryID,
                                 Image2 = p.Image2,
                                 BytesImage2 = p.BytesImage2,
                                 Image3 = p.Image3,
                                 BytesImage3 = p.BytesImage3,
                                 ShopLink = p.ShopLink,
                                 CityID = p.CityID,
                                 SchoolID = p.SchoolID,
                                 SchoolName = s.SchoolName,
                                 FoodCategoryName = f.FoodCategoryName
                             };
                if (SchoolID == 1)
                {
                    ViewBag.name = "國立台灣大學";
                    ViewBag.add1 = "台北市大安區忠孝東路四段235號2樓";
                    ViewBag.add2 = "台北市大安區安和路二段217巷2弄3號2樓";
                    ViewBag.add3 = "台北市中正區羅斯福路三段100之1號";
                }
                else if (SchoolID == 6)
                {
                    ViewBag.name = "國立台灣藝術大學";
                    ViewBag.add1 = "新北市中和區立德街56號";
                    ViewBag.add2 = "新北市板橋區民權路3號";
                }
                return View(result.ToList());
            }


        }

        public ActionResult PartialList()
        {
            var result = from p in db.Shop
                         join s in db.School on p.SchoolID equals s.SchoolID
                         join f in db.FoodCategory on p.FoodCategoryID equals f.FoodCategoryID
                         select new Temp
                         {
                             ShopID = p.ShopID,
                             Address = p.Address,
                             Phone = p.Phone,
                             Image1 = p.Image1,
                             BytesImage1 = p.BytesImage1,
                             Description = p.Description,
                             Cost = p.Cost,
                             BusinessTime = p.BusinessTime,
                             ShopName = p.ShopName,
                             FoodCategoryID = p.FoodCategoryID,
                             Image2 = p.Image2,
                             BytesImage2 = p.BytesImage2,
                             Image3 = p.Image3,
                             BytesImage3 = p.BytesImage3,
                             ShopLink = p.ShopLink,
                             CityID = p.CityID,
                             SchoolID = p.SchoolID,
                             SchoolName = s.SchoolName,
                             FoodCategoryName = f.FoodCategoryName
                         };
            return PartialView(result.ToList());
        }


        public ActionResult PartialDrop()
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

    }
    public class Temp
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



        [DisplayName("學校名稱")]
        public string SchoolName { get; set; }

        [DisplayName("分類名稱")]

        public string FoodCategoryName { get; set; }
    }
}