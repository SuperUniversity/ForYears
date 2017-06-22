﻿using FourYears.Areas.BookStoreAreas.Models;
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
        private IRepository_BookStoreSystemModel<BookStoreAdmin> db_BookStoreAdmin = new Repository_BookStoreSystemModel<BookStoreAdmin>();

        //private SuperUniversityEntities _entity = new SuperUniversityEntities();   //已寫Repository 不直接更動DB

        public ActionResult AdminPage()
        {
            if (Request.Cookies["AdminID"] == null)
            {
                return RedirectToAction("AdminLogin", "Account", new { Area = "BookStoreAreas" });
            }

            int id = int.Parse(Request.Cookies["AdminID"].Value);
            var result = db_BookStoreAdmin.GetByID(id);
            return View(result);
        }


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
            db_Publisher.Delete(db_Publisher.GetByID(id));
            return RedirectToAction("AdminPublisherIndex");
        }

        public ActionResult AllBookIndex()
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


    }
}