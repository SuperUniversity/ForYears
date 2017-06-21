using FourYears.Areas.BookStoreAreas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.BookStoreAreas.Controllers
{
    public class CartController : Controller
    {
        // GET: BookStoreAreas/Cart
        public ActionResult GetCart()
        {
            return PartialView("Cart_PartialView");
        }

        public ActionResult AddToCart(int id)
        {
            var currentCart = Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("Cart_PartialView");
        }
    }
}