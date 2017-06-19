using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.JobArea.Models
{
    [MetadataType(typeof(EmployerCompanyMetadata))]
    public partial class EmployerCompany
    {
        public class EmployerCompanyMetadata
        {

            [DisplayName("公司編號")]
            public int CompanyID { get; set; }
            [DisplayName("公司名稱")]
            [Required(ErrorMessage = "公司名稱不可為白")]
            public string CompanyName { get; set; }
            [DisplayName("公司地址")]
            [Required(ErrorMessage = "公司地址不可為白")]
            public string CompanyAdress { get; set; }
            [DisplayName("聯絡人")]
            [Required(ErrorMessage = "聯絡人不可為白")]
            public string EmployerName { get; set; }
            [DisplayName("聯絡電話")]
            [Required(ErrorMessage = "聯絡電話不可為白")]
            public string EmployerPhone { get; set; }
            [DisplayName("Email")]
            [Required(ErrorMessage = "Email不可為白")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "格式不正確")]
            public string EmployerMail { get; set; }
            [DisplayName("密碼")]
            [Required(ErrorMessage = "密碼不可為白")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [DisplayName("密碼確認")]
            [Compare("Password", ErrorMessage = "密碼不一致")]
            public string ConfirmPassword { get; set; }

        }
    }
}