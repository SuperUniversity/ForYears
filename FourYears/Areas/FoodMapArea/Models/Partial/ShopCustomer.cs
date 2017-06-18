using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.FoodMapArea.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class ShopCustomer
    {
        public class CustomerMetadata
        {
            [DisplayName("會員編號")]
            public int CustomerID { get; set; }
            [DisplayName("會員姓名")]
            [Required(ErrorMessage = "請輸入您的姓名")]
            public string FullName { get; set; }
            [DisplayName("會員帳號")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "電子信箱格式錯誤")]
            [Required(ErrorMessage = "請輸入電子信箱")]
            public string EmailAddress { get; set; }
            [DisplayName("會員密碼")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "請輸入密碼")]
            public string Password { get; set; }
        }
    }
}