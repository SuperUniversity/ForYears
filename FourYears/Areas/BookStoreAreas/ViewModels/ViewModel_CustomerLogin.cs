using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.BookStoreAreas.ViewModels
{
    public class ViewModel_CustomerLogin
    {

        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }

        [DisplayName("保持登入狀態")]
        public bool RememberMe { get; set; }

    }
}