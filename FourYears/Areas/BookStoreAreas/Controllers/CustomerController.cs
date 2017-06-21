using FourYears.Areas.BookStoreAreas.Models;
using FourYears.Areas.BookStoreAreas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.BookStoreAreas.Controllers
{
    public class CustomerController : Controller
    {
        // GET: BookStoreAreas/Customer
        // GET: BookStoreAreas/Customer
        //private IRepository_BookStoreSystemModel<ViewModel_BookInformation> db_Book = new Repository_BookStoreSystemModel<ViewModel_BookInformation>();
        private IRepository_BookStoreSystemModel<Author> db_Author = new Repository_BookStoreSystemModel<Author>();
        private IRepository_BookStoreSystemModel<Book> db_Book = new Repository_BookStoreSystemModel<Book>();
        private IRepository_BookStoreSystemModel<SubCategory> db_SubCategory = new Repository_BookStoreSystemModel<SubCategory>();
        private IRepository_BookStoreSystemModel<Publisher> db_Publisher = new Repository_BookStoreSystemModel<Publisher>();
        private IRepository_BookStoreSystemModel<Customer> db_Customer = new Repository_BookStoreSystemModel<Customer>();

        private superuniversityEntities4 _entity = new superuniversityEntities4();

        //test
        public ActionResult Browse(int id = 0)
        {
            ViewBag.Message = string.Format("全部商品");
            //bookinfo.Author = db_Author.GetAll();
            //bookinfo.Book = db_Book.GetAll().Where(b => b.SubCategoryID == id).ToList();

            if (id != 0)
            {
                var resultByID = from book in db_Book.GetAll().Where(b => b.SubCategoryID == id)
                                 join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                                 join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                                 join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
                                 select new ViewModel_BookInformation
                                 {
                                     BookID = book.BookID,
                                     BookName = book.BookName,
                                     AuthorName = author.AuthorName,
                                     SubCategoryName = subCategory.SubCategoryName,
                                     PublisherName = publisher.PublisherName,
                                     ListPrice = book.ListPrice,
                                     SalePrice = book.SalePrice,
                                 };

                return View(resultByID.ToList());

            }

            var result = from book in db_Book.GetAll()
                         join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
                         join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
                         join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
                         select new ViewModel_BookInformation
                         {
                             BookID = book.BookID,

                             BookName = book.BookName,
                             AuthorName = author.AuthorName,
                             SubCategoryName = subCategory.SubCategoryName,
                             PublisherName = publisher.PublisherName,
                             ListPrice = book.ListPrice,
                             SalePrice = book.SalePrice,
                         };

            return View(result.ToList());
        }

        //[HttpPost]
        //public ActionResult Browse(int id=0)
        //{
        //    ViewModel_BookInfo bookinfo = new ViewModel_BookInfo();

        //    ViewBag.Message = string.Format("全部商品");
        //    //bookinfo.Author = db_Author.GetAll();
        //    //bookinfo.Book = db_Book.GetAll().Where(b => b.SubCategoryID == id).ToList();
        //    var result = from book in db_Book.GetAll().Where(b => b.SubCategoryID == id)
        //                 join author in db_Author.GetAll() on book.AuthorID equals author.AuthorID
        //                 join subCategory in db_SubCategory.GetAll() on book.SubCategoryID equals subCategory.SubCategoryID
        //                 join publisher in db_Publisher.GetAll() on book.PublisherID equals publisher.PublisherID
        //                 select new ViewModel_BookInformation
        //                 {
        //                     BookName = book.BookName,
        //                     AuthorName = author.AuthorName,
        //                     SubCategoryName = subCategory.SubCategoryName,
        //                     PublisherName = publisher.PublisherName,
        //                     ListPrice = book.ListPrice,
        //                     SalePrice = book.SalePrice,
        //                     Discount = book.Discount,
        //                     DiscountPrice = book.DiscountPrice,
        //                 };

        //    return View(result.ToList());
        //}

        public ActionResult CustomerProfile()    //顧客會員:管理介面(登入成功後跳轉)
        {
            if (Request.Cookies["CustomerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "Account", new { Area = "BookStoreAreas" });
            }

            int id = int.Parse(Request.Cookies["CustomerID"].Value);
            var result = db_Customer.GetByID(id);
            return View(result);
        }

        [HttpGet]
        public ActionResult CustomerChangePassword()  //Get進入→顧客會員:修改密碼
        {
            if (Request.Cookies["CustomerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "Account", new { Area = "BookStoreAreas" });
            }
            int id = int.Parse(Request.Cookies["CustomerID"].Value);
            Customer customer = db_Customer.GetByID(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult CustomerChangePassword(Customer customer)  // Post進入→顧客會員:重複更改密碼
        {
            db_Customer.Update(customer);
            return RedirectToAction("CustomerProfile", "Customer", new { Area = "BookStoreAreas" });
        }


        [HttpGet]
        public ActionResult CustomerEditProfile()    // Get進入→顧客會員:編輯個人資料
        {
            if (Request.Cookies["CustomerID"] == null)
            {
                return RedirectToAction("CustomerLogin", "Account", new { Area = "BookStoreAreas" });
            }
            int id = int.Parse(Request.Cookies["CustomerID"].Value);
            Customer customer = db_Customer.GetByID(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult CustomerEditProfile(Customer customer)   // Post進入→顧客會員:更改個人資料
        {
            db_Customer.Update(customer);
            return RedirectToAction("CustomerProfile", "Customer", new { Area = "BookStoreAreas" });
        }



        public ActionResult GetBookImage1(int id = 1)   //讀取商品圖片1
        {
            Book book = db_Book.GetByID(id);
            byte[] img = book.BytesImage1;
            return File(img, "image/jpeg");
        }
        public ActionResult GetBookImage2(int id = 1)   //讀取商品圖片2
        {
            Book book = db_Book.GetByID(id);
            byte[] img = book.BytesImage2;
            return File(img, "image/jpeg");
        }
        public ActionResult GetBookImage3(int id = 1)   //讀取商品圖片3
        {
            Book book = db_Book.GetByID(id);
            byte[] img = book.BytesImage3;
            return File(img, "image/jpeg");
        }
    }
}