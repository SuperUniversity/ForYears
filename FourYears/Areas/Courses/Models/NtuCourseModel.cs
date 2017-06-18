//using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcClient.Areas.Courses.Models
{
    public class NtuCourseModel
    {
        public string 課程名稱 { get; set; }
        public string 授課教師 { get; set; }
        public string 授課對象 { get; set; }
        public string 時間教室 { get; set; }


        public string 班次 { get; set; }
        public string 全半年 { get; set; }
        public string 學分 { get; set; }
        public string 必選修 { get; set; }
        public string 總人數 { get; set; }
        public string 選課限制條件 { get; set; }
        public string 課程網頁 { get; set; }
        public string 備註 { get; set; }


        public string _id { get; set; }
        public string 本學期我預計要選的課程 { get; set; }
        public string 課號 { get; set; }
        public string 流水號 { get; set; }
        public string 加選方式 { get; set; }
        public string 課程識別碼 { get; set; }


        public List<Comment> commentdata { get; set; }
        public List<Ranking> rankingdata { get; set; }
        public DateTime lastModified { get; set; }
    }
}