using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Models.Partial
{
    [MetadataType(typeof(AspNetUserLoginLogMetadata))]
    public partial class AspNetUserLoginLog
    {
        public class AspNetUserLoginLogMetadata
        {
            [DisplayName("登入編號")]
            public int id { get; set; }
            [DisplayName("使用者編號")]
            public string userID { get; set; }
            [DisplayName("登入時間")]
            public Nullable<System.DateTime> loginTime { get; set; }
        }
    }
}