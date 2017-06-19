using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClient.Areas.Courses.Models
{
    public class AllCollegeCourseModel
    {
        //[HiddenInput(DisplayValue =false)]
        public string _id { get; set; }
        [DisplayName("學校")]
        public string College { get; set; }
        [DisplayName("課號")]
        public string Identity { get; set; }
        [DisplayName("最後更新學期")]
        public string Semester { get; set; }
        [DisplayName("課程名稱")]
        public string CourseName { get; set; }
        [DisplayName("老師")]
        public string Teacher { get; set; }
        [DisplayName("時間教室")]
        public string TimeAndClassroom { get; set; }
        [DisplayName("開課系所")]
        public string Major { get; set; }
        [DisplayName("必修")]
        public string Required { get; set; }
        [DisplayName("人數上限")]
        public string MaxNum { get; set; }
        [DisplayName("學分")]
        public string Credit { get; set; }
        [DisplayName("修課限制")]
        public string Limitation { get; set; }
        [DisplayName("備註")]
        public string Note { get; set; }

        public List<Comment> commentdata { get; set; }
        public List<Ranking> rankingdata { get; set; }
        public DateTime lastModified { get; set; }
    }


    public class Comment
    {
        public string commentID { get; set; }
        public string userID { get; set; }

        //[DisplayName("姓名")]
        //[Required(ErrorMessage = "請輸入姓名")]
        //[DataType(DataType.Text)]
        public string name { get; set; }

        //[DisplayName("電子郵件")]
        //[EmailAddress(ErrorMessage = "不符合電子郵件格式")]
        //[Required(ErrorMessage = "請輸入電子郵件")]
        //[DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DisplayName("評論內容")]
        [Required(ErrorMessage = "請輸入留言內容")]
        [DataType(DataType.MultilineText)]
        public string commentstring { get; set; }

        [DisplayName("匿名評論")]
        public bool anonym { get; set; }

        public bool hidden { get; set; }

        [DisplayName("允許他人以電子郵件聯絡")]
        public bool allowEmailContact { get; set; }

        public int likes { get; set; }

        public int dislikes { get; set; }

        public DateTime lastModified { get; set; }
    }

    public class Ranking
    {
        public string rankingID { get; set; }

        public string userID { get; set; }

        [DisplayName("深度")]
        [Range(1, 10, ErrorMessage = "必須介在1到10之間")]
        public int deepness { get; set; }

        [DisplayName("涼度")]
        [Range(1, 10, ErrorMessage = "必須介在1到10之間")]
        public int relaxing { get; set; }

        [DisplayName("甜度")]
        [Range(1, 10, ErrorMessage = "必須介在1到10之間")]
        public int sweetness { get; set; }

        public DateTime lastModified { get; set; }
    }

    public class User_Ranking
    {
        public string UserID { get; set; }
        public string CourseID { get; set; }
        public string RankingID { get; set; }
        public DateTime lastModified { get; set; }
    }
    public class User_Comment
    {
        public string UserID { get; set; }
        public string CourseID { get; set; }
        public string CommentID { get; set; }
        public DateTime lastModified { get; set; }
    }
    public class User_Favorite
    {
        public string UserID { get; set; }
        public string CourseID { get; set; }
        public DateTime lastModified { get; set; }
    }
}