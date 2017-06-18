using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FourYears.Areas.JobArea.Models.Partails
{
    [MetadataType(typeof(JobMetadata))]
    public partial class Job
    {
        public class JobMetadata
        {
            [DisplayName("工作編號")]
            public int JobID { get; set; }

            [DisplayName("工作名稱")]
            [Required(ErrorMessage = "工作名稱不可為白")]
            public string JobName { get; set; }

            [DisplayName("公司編號")]
            public int CompanyID { get; set; }
            [DisplayName("可上班日")]
            [DataType(DataType.Date)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public Nullable<System.TimeSpan> JobStartTime { get; set; }

            [DisplayName("工作時段")]
            public int TimeID { get; set; }
            [DisplayName("時薪")]
            [DisplayFormat(DataFormatString = "{0:C0}")]
            public Nullable<decimal> PayPerHour { get; set; }
            [DisplayName("工作描述")]
            [DataType(DataType.MultilineText)]
            [Required(ErrorMessage = "工作描述不可為白")]
            public string Description { get; set; }
            [DisplayName("上班地點")]
            [Required(ErrorMessage = "上班地點不可為白")]
            public string Workplace { get; set; }
            [DisplayName("工作圖片")]
            public byte[] Image { get; set; }
            [DisplayName("圖片網址")]
            public string ImageWebSite { get; set; }
        }
    }
}