//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcClient.Areas.Courses.Models
{
    public class NckuCourseModel
    {
        public string 課程名稱 { get; set; }
        public string 教師姓名 { get; set; }
        public string 系所名稱 { get; set; }
        public string 班別 { get; set; }
        public string 組別 { get; set; }
        public string 年級 { get; set; }

        //[BsonSerializer(typeof(MyBsonSerializer))]
        public string 已選課人數 { get; set; }

        //[BsonSerializer(typeof(MyBsonSerializer))]
        public string 餘額 { get; set; }
        public string 時間 { get; set; }
        public string 教室 { get; set; }

        //[BsonSerializer(typeof(MyBsonSerializer))]
        public string 學分 { get; set; }
        public string 選必修 { get; set; }
        public string 限選條件 { get; set; }
        public string 英語授課 { get; set; }
        public string 跨領域學分學程 { get; set; }
        public string Moocs { get; set; }
        public string 業界專家參與 { get; set; }
        public string 備註 { get; set; }



        public string _id { get; set; }
        public string 類別 { get; set; }

        //[BsonSerializer(typeof(MyBsonSerializer))]
        public string 序號 { get; set; }
        //public int 序號 { get; set; }
        public string 系號 { get; set; }
        public string 課程碼 { get; set; }
        public string 分班碼 { get; set; }
        public string 屬性碼 { get; set; }


        public List<Comment> commentdata { get; set; }
        public List<Ranking> rankingdata { get; set; }
        public DateTime lastModified { get; set; }
    }
}