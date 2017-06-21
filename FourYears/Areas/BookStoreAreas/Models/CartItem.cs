using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.Models
{
    [Serializable]   //可序列化
    public class CartItem
    {
        public int CartItemID { get; set; }
        public string CartItemName { get; set; }
        public decimal CartItemPrice { get; set; }
        public int Quantity { get;set; }
        public decimal Amount
        {
            get
            {
                return this.CartItemPrice * this.Quantity;
            }
        }
    }
}