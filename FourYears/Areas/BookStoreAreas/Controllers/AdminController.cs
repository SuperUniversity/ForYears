using FourYears.Areas.BookStoreAreas.Models;
using FourYears.Areas.BookStoreAreas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace FourYears.Areas.BookStoreAreas.Controllers
{
    public class AdminController : Controller
    {
        // GET: BookStoreAreas/Admin
        // GET: BookStoreAdmin
        private IRepository_BookStoreSystemModel<Book> db_Book = new Repository_BookStoreSystemModel<Book>();
        private IRepository_BookStoreSystemModel<SubCategory> db_SubCategory = new Repository_BookStoreSystemModel<SubCategory>();
        private IRepository_BookStoreSystemModel<MainCategory> db_MainCategory = new Repository_BookStoreSystemModel<MainCategory>();
        private IRepository_BookStoreSystemModel<Publisher> db_Publisher = new Repository_BookStoreSystemModel<Publisher>();
        private IRepository_BookStoreSystemModel<Author> db_Author = new Repository_BookStoreSystemModel<Author>();
        //private SuperUniversityEntities _entity = new SuperUniversityEntities();   //已寫Repository 不直接更動DB

        public ActionResult AdminPublisherIndex()   //後台管理者 觀看廠商名單
        {
            List<Publisher> Publisher = db_Publisher.GetAll().ToList();
            return View(Publisher);
        }

        [HttpGet]                                         //Get:後台管理者 修改廠商資料
        public ActionResult AdminPublisherEdit(int id = 0)
        {
            Publisher publisher = db_Publisher.GetByID(id);
            return View(publisher);
        }

        [HttpPost]                                         //Post:後台管理者 修改廠商資料
        public ActionResult AdminPublisherEdit(Publisher publisher)
        {
            db_Publisher.Update(publisher);
            return RedirectToAction("AdminPublisherIndex");
        }

        public ActionResult AdminPublisherDelete(int id = 0)
        {
            DialogResult result = MessageBox.Show("是否確定刪除此筆資料?", "刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                db_Publisher.Delete(db_Publisher.GetByID(id));
                return RedirectToAction("AdminPublisherIndex");
            }
            return RedirectToAction("AdminPublisherIndex");
        }

        public ActionResult Index()
        {
            //List<Book> Book = db_Book.GetAll().ToList();
            var result = from book in db_Book.GetAll()                         //Linq語法 join多張Table
                         join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                         join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                         join mainCategory in db_MainCategory.GetAll() on subCategory.MainCategoryID equals mainCategory.MainCategoryID
                         join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
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
                             Language = book.Language,
                             ListPrice = book.ListPrice,
                             SalePrice = book.SalePrice,
                         };
            return View(result.ToList());
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult MainCategories_Partial()    //建立Partial View
        {
            var mainCategories = db_MainCategory.GetAll();
            SelectList SelectList_MainCategory = new SelectList(mainCategories, "MainCategoryID", "MainCategoryName");     //建立Select集合
            ViewBag.MainCategoryDatas = SelectList_MainCategory;    //將集合傳入ViewBag，Return回Partial View

            return PartialView();
        }


        [HttpPost]
        public JsonResult SubCategories_JsonResult(int MainCategoryID)  //第二層DropdownList 回傳JsonResult
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
    }
}