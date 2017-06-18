//using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcClient.Areas.Courses.Models
{
    public class NctuCourseModel
    {

        public string 課程名稱 { get; set; }
        public string 開課教師 { get; set; }
        public string 上課時間及教室 { get; set; }


        public string _id { get; set; }
        public string 學期別 { get; set; }
        public string 英文課程名稱 { get; set; }
        public string 永久課號 { get; set; }
        public string 選別 { get; set; }
        public string 時數 { get; set; }
        public string 學分 { get; set; }
        public string 課號 { get; set; }
        public string 備註 { get; set; }
        public string 摘要 { get; set; }
        public string 人數上限 { get; set; }
        public string 修課人數 { get; set; }

        public List<Comment> commentdata { get; set; }
        public List<Ranking> rankingdata { get; set; }
        public DateTime lastModified { get; set; }
    }
}