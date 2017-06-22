using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.ViewModels
{
    public class ViewModel_AdminLogin
    {
        [DisplayName("管理員帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string AdminName { get; set; }
        [DisplayName("管理員密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "請輸入密碼")]
        public string AdminPassword { get; set; }
    }
}