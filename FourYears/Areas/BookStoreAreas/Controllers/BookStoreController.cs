using FourYears.Areas.BookStoreAreas.Models;
using FourYears.Areas.BookStoreAreas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.BookStoreAreas.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStoreAreas/BookStore
        private IRepository_BookStoreSystemModel<Book> db_Book = new Repository_BookStoreSystemModel<Book>();
        private IRepository_BookStoreSystemModel<SubCategory> db_SubCategory = new Repository_BookStoreSystemModel<SubCategory>();
        private IRepository_BookStoreSystemModel<MainCategory> db_MainCategory = new Repository_BookStoreSystemModel<MainCategory>();
        private IRepository_BookStoreSystemModel<Publisher> db_Publisher = new Repository_BookStoreSystemModel<Publisher>();
        private IRepository_BookStoreSystemModel<Author> db_Author = new Repository_BookStoreSystemModel<Author>();
        private IRepository_BookStoreSystemModel<ViewModel_BookInformation> db_vm_BookInformation = new Repository_BookStoreSystemModel<ViewModel_BookInformation>();
        private superuniversityEntities4 _entity = new superuniversityEntities4();


        public ActionResult BookIndex() //廠商 觀看自己的上架書單
        {
            int id = int.Parse(Request.Cookies["PublisherID"].Value);

            var result = from book in db_Book.GetAll()                         //Linq語法 join多張Table
                         join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                         join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                         join mainCategory in db_MainCategory.GetAll() on subCategory.MainCategoryID equals mainCategory.MainCategoryID
                         join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
                         where book.PublisherID == id
                         select new ViewModel_BookInformation
                         {
                             BookID = book.BookID,
                             BookName = book.BookName,
                             AuthorName = author.AuthorName,
                             PublisherName = publisher.PublisherName,
                             MainCategoryName = mainCategory.MainCategoryName,
                             SubCategoryName = subCategory.SubCategoryName,
                             PublishDate = book.PublishDate,
                             OnShelfDate = book.OnShelfDate,
                             BookDescription = book.BookDescription,
                             AuthorDescription = book.AuthorDescription,
                             OtherImformation = book.OtherImformation,
                             Language = book.Language,
                             ListPrice = book.ListPrice,
                             SalePrice = book.SalePrice,

                         };

            ViewBag.Result = TempData["Result"];
            return View(result.ToList());
        }

        [HttpGet]
        public ActionResult BookInsert(string errorstring = "")          //Get進入:廠商 新增書本資料
        {
            if (errorstring != "")
            {
                ViewBag.Message = "請至少選擇一張商品圖片";
            }

            ViewBag.Author = db_Author.GetAll();
            return View();
        }
        public ActionResult Book_MainCategories_PartialView()    //建立Partial View
        {
            var mainCategories = db_MainCategory.GetAll();
            SelectList SelectList_MainCategory = new SelectList(mainCategories, "MainCategoryID", "MainCategoryName");     //建立Select集合
            ViewBag.MainCategoryDatas = SelectList_MainCategory;    //將集合傳入ViewBag，Return回Partial View

            return PartialView();
        }

        [HttpPost]
        public JsonResult Book_SubCategories_JsonResult(int MainCategoryID)  //第二層DropdownList 回傳JsonResult
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            if (MainCategoryID > 0)
            {
                var subCategories = this.GetSubCategory(MainCategoryID);
                if (subCategories.Count() > 0)
                {
                    foreach (var subCategory in subCategories)
                    {
                        items.Add(new KeyValuePair<string, string>(
                            subCategory.SubCategoryID.ToString(),
                            subCategory.SubCategoryName.ToString()));
                    }
                }
            }
            return this.Json(items);
        }

        private IEnumerable<SubCategory> GetSubCategory(int MainCategoryID)  //根據傳入的MainCategoryID 找到包含的SubCategory
        {
            var result_SubCategory = db_SubCategory.GetAll().Where(sub => sub.MainCategoryID == MainCategoryID);
            return result_SubCategory.ToList();
        }

        [HttpPost]
        public ActionResult BookInsert(Book Book, Author Author, MainCategory MainCategory, SubCategory SubCategory, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)          //Post進入:廠商 新增書本資料
        {
            Book.PublisherID = int.Parse(Request.Cookies["PublisherID"].Value);

            if (Image1 != null)
            {
                string strPath1 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image1.FileName;
                Image1.SaveAs(strPath1);
                var imgSize1 = Image1.ContentLength;
                byte[] imgByte1 = new byte[imgSize1];
                Image1.InputStream.Read(imgByte1, 0, imgSize1);
                Book.Image1 = Image1.FileName;
                Book.BytesImage1 = imgByte1;

                if (Image2 != null)
                {
                    string strPath2 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image2.FileName;
                    Image2.SaveAs(strPath2);
                    var imgSize2 = Image2.ContentLength;
                    byte[] imgByte2 = new byte[imgSize2];
                    Image2.InputStream.Read(imgByte2, 0, imgSize2);
                    Book.Image2 = Image2.FileName;
                    Book.BytesImage2 = imgByte2;
                }
                if (Image3 != null)
                {
                    string strPath3 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image3.FileName;
                    Image3.SaveAs(strPath3);
                    var imgSize3 = Image3.ContentLength;
                    byte[] imgByte3 = new byte[imgSize3];
                    Image3.InputStream.Read(imgByte3, 0, imgSize3);
                    Book.Image3 = Image3.FileName;
                    Book.BytesImage3 = imgByte3;
                }
                //db_vm_BookInformation.Create(vm_Book);
                db_Book.Create(Book);
                TempData["Result"] = string.Format("商品{0}新增成功", Book.BookName);
                return RedirectToAction("BookIndex", "BookStore", new { Area = "BookStoreAreas" });
            }

            return RedirectToAction("BookInsert", "BookStore", new { Area = "BookStoreAreas", errorstring = "請至少選擇一張商品圖片" });  //回到 [HttpGet] BookInsert
            //return View();   //Todo..... RedirectToAction會清空所填資料
        }

        [HttpGet]
        public ActionResult BookEdit(int id = 0, string errorstring = "")   //Get方法→修改商品資訊
        {
            //ViewModel_BookInformation vm_book = new ViewModel_BookInformation();
            //vm_book.books = db_Book.GetAll();
            //vm_book.subCategories = db_SubCategory.GetAll().Where(book => book.SubCategoryID == id);
            ////vm_book.mainCategories = db_MainCategory.GetAll().Where(book =>book.MainCategoryID==)
            if (errorstring != "")
            {
                ViewBag.Message = "請至少選擇一張商品圖片";
            }

            Book Book = db_Book.GetByID(id);
            ViewBag.subCategory = db_SubCategory.GetAll();
            ViewBag.mainCategory = db_MainCategory.GetAll();
            ViewBag.author = db_Author.GetAll();

            return View(Book);
        }

        [HttpPost]     //Post方法→修改商品資訊
        public ActionResult BookEdit(Book Book, Author Author, MainCategory MainCategory, SubCategory SubCategory, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {
            Book.PublisherID = int.Parse(Request.Cookies["PublisherID"].Value);

            if (Image1 != null)
            {
                string strPath1 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image1.FileName;
                Image1.SaveAs(strPath1);
                var imgSize1 = Image1.ContentLength;
                byte[] imgByte1 = new byte[imgSize1];
                Image1.InputStream.Read(imgByte1, 0, imgSize1);
                Book.Image1 = Image1.FileName;
                Book.BytesImage1 = imgByte1;

                if (Image2 != null)
                {
                    string strPath2 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image2.FileName;
                    Image2.SaveAs(strPath2);
                    var imgSize2 = Image2.ContentLength;
                    byte[] imgByte2 = new byte[imgSize2];
                    Image2.InputStream.Read(imgByte2, 0, imgSize2);
                    Book.Image2 = Image2.FileName;
                    Book.BytesImage2 = imgByte2;
                }
                if (Image3 != null)
                {
                    string strPath3 = Request.PhysicalApplicationPath + @"Areas/BookStoreAreas/BookImages/" + Image3.FileName;
                    Image3.SaveAs(strPath3);
                    var imgSize3 = Image3.ContentLength;
                    byte[] imgByte3 = new byte[imgSize3];
                    Image3.InputStream.Read(imgByte3, 0, imgSize3);
                    Book.Image3 = Image3.FileName;
                    Book.BytesImage3 = imgByte3;
                }
                db_Book.Update(Book);
                return RedirectToAction("BookIndex", "BookStore", new { Area = "BookStoreAreas" });
            }

            //return View();
            return RedirectToAction("BookEdit", "BookStore", new { Area = "BookStoreAreas", errorstring = "請至少選擇一張商品圖片" });  //回到 [HttpGet] BookInsert
            //return View();   //Todo..... RedirectToAction會清空所填資料
        }

        public ActionResult BookDelete(int id = 0)   //刪除:下架商品
        {
            db_Book.Delete(db_Book.GetByID(id));
            return RedirectToAction("BookIndex");
        }


        public ActionResult PublisherProfile()    //顧客會員:管理介面(登入成功後跳轉)
        {
            if (Request.Cookies["PublisherID"] == null)
            {
                return RedirectToAction("PublisherLogin", "Account", new { Area = "BookStoreAreas" });
            }

            int id = int.Parse(Request.Cookies["PublisherID"].Value);
            var result = db_Publisher.GetByID(id);
            return View(result);
        }

        public ActionResult BookDetail(int id = 1)    //單筆書本資訊
        {
            if (id != 0)
            {
                ViewBag.bid = id;
                var resultByID = from book in db_Book.GetAll()
                                 join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                                 join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                                 join mainCategory in db_MainCategory.GetAll() on subCategory.MainCategoryID equals mainCategory.MainCategoryID
                                 join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
                                 where book.BookID == id
                                 select new ViewModel_BookInformation
                                 {
                                     BookID = book.BookID,
                                     BookName = book.BookName,
                                     AuthorName = author.AuthorName,
                                     MainCategoryName = mainCategory.MainCategoryName,
                                     SubCategoryName = subCategory.SubCategoryName,
                                     PublisherName = publisher.PublisherName,
                                     Language = book.Language,
                                     BookDescription = book.BookDescription,
                                     AuthorDescription = book.AuthorDescription,
                                     OtherImformation = book.OtherImformation,
                                     PublishDate = book.PublishDate,
                                     OnShelfDate = book.OnShelfDate,
                                     ListPrice = book.ListPrice,
                                     SalePrice = book.SalePrice,
                                     BytesImage1 = book.BytesImage1,
                                     BytesImage2 = book.BytesImage2,
                                     BytesImage3 = book.BytesImage3
                                 };

                return View(resultByID.ToList().First());

            }
            //ViewBag.bid = id;
            var result = from book in db_Book.GetAll()
                         join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                         join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                         join mainCategory in db_MainCategory.GetAll() on subCategory.MainCategoryID equals mainCategory.MainCategoryID
                         join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
                         where book.BookID == id
                         select new ViewModel_BookInformation
                         {
                             BookID = book.BookID,
                             BookName = book.BookName,
                             AuthorName = author.AuthorName,
                             MainCategoryName = mainCategory.MainCategoryName,
                             SubCategoryName = subCategory.SubCategoryName,
                             PublisherName = publisher.PublisherName,
                             Language = book.Language,
                             BookDescription = book.BookDescription,
                             AuthorDescription = book.AuthorDescription,
                             OtherImformation = book.OtherImformation,
                             PublishDate = book.PublishDate,
                             OnShelfDate = book.OnShelfDate,
                             ListPrice = book.ListPrice,
                             SalePrice = book.SalePrice,
                             BytesImage1 = book.BytesImage1,
                             BytesImage2 = book.BytesImage2,
                             BytesImage3 = book.BytesImage3
                         };


            return View(result.ToList().First());

        }
    }
}