using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.Models
{
    [Serializable]   //可序列化
    public class Cart:IEnumerable<CartItem>
    {
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }

        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems.Where(s => s.CartItemID == ProductId).Select(s => s).FirstOrDefault();
            if(findItem == default(CartItem))
            {
                using (superuniversityEntities4 db = new superuniversityEntities4())
                {
                    var product = (from s in db.Book
                                   where s.BookID == ProductId
                                   select s).FirstOrDefault();
                    if(product != default(Book))
                    {
                        this.AddProduct(ProductId);
                    }

                }
            }
            else
            {
                findItem.Quantity += 1;
            }
            return true;
        }

        private bool AddProduct(Book product)
        {
            var cartItem = new CartItem()
            {
                CartItemID = product.BookID,
                CartItemName = product.BookName,
                CartItemPrice = product.SalePrice,
                Quantity = 1
            };

            this.cartItems.Add(cartItem);
            return true;
        }

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }
    }
}