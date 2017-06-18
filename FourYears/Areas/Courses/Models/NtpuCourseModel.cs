//using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcClient.Areas.Courses.Models
{
    public class NtpuCourseModel
    {
        public string _id { get; set; }
        public string 科目名稱 { get; set; }
        public string 授課教師 { get; set; }
        public string 上課時間教室 { get; set; }
        public string 開課系所 { get; set; }
        public string 學分 { get; set; }

        public string 應修系級 { get; set; }
        public string 必選修別 { get; set; }
        public string 限修總計人數 { get; set; }

        public string 是否開放加簽 { get; set; }
        public string 加簽人數上限 { get; set; }
        public string 繳費時數 { get; set; }
        public string 授課語別 { get; set; }
        public string 備註 { get; set; }


        public string 學期 { get; set; }
        public string 學年 { get; set; }
        public string 課程流水號 { get; set; }
        public string 已選總計人數 { get; set; }
        public string 已核准人數 { get; set; }
        public string 類別 { get; set; }
        public string 待分發人數 { get; set; }

        public List<Comment> commentdata { get; set; }
        public List<Ranking> rankingdata { get; set; }
        public DateTime lastModified { get; set; }

    }
}