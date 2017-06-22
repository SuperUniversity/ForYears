using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FourYears.ViewModel
{
    public class UserLoginLogViewModel
    {
        [DisplayName("登入編號")]
        public int id { get; set; }
        [DisplayName("使用者編號")]
        public string userID { get; set; }
        [DisplayName("暱稱")]
        public string NickName { get; set; }
        [DisplayName("信箱")]
        public string Email{ get; set; }
        [DisplayName("允許信箱聯絡")]
        public bool AllowEmailContact { get; set; }
        [DisplayName("登入時間")]
        public Nullable<System.DateTime> loginTime { get; set; }
    }
}