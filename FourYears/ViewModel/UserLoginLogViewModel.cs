using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.ViewModel
{
    public class UserLoginLogViewModel
    {
        [Key]
        public int LogId { get; set; }
        
        [DisplayName("使用者編號")]
        public string UserId { get; set; }
        [DisplayName("暱稱")]
        public string NickName { get; set; }
        [DisplayName("角色")]
        public string Role { get; set; }
        [DisplayName("信箱")]
        public string Email{ get; set; }
        [DisplayName("登入時間")]
        public Nullable<System.DateTime> loginTime { get; set; }
    }
}