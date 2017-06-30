using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.ViewModel
{
    public class AccountManagerViewModel
    {
        [Key]
        public string Id { get; set; }
        public string ActualName { get; set; }
        [Display(Name ="暱稱")]
        public string NickName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name ="帳號")]
        public string UserName { get; set; }
        [Display(Name ="郵件確認")]
        public bool EmailConfirmed { get; set; }
        [Display(Name ="允許郵件通知")]
        public bool AllowEmailContact { get; set; }
        [Display(Name ="登入失敗次數")]
        public int AccessFailedCount { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Display(Name ="創建時間")]
        public DateTime CreateTime { get; set; }

        public int UniversityId { get; set; }
        [Display(Name = "學校")]
        public IEnumerable<System.Web.Mvc.SelectListItem> UniversitySelectList { get; set; }

        public string RoleId { get; set; }
        [Display(Name = "角色")]
        public IEnumerable<System.Web.Mvc.SelectListItem> RoleSelectList { get; set; }
    }
}