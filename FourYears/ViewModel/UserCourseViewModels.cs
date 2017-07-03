using MvcClient.Areas.Courses.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FourYears.Areas.Courses.ViewModel
{
    public class UserCourseViewModels
    {

    }

    public class UserCommentViewModel
    {
        [Key]
        [Column(Order = 1)]
        public int User_CommentId { get; set; }

        [Column(Order = 2)]
        public string UserId { get; set; }

        [DisplayName("暱稱")]
        [Column(Order = 3)]
        public string NickName { get; set; }

        [Column(Order = 4)]
        public string Email { get; set; }

        [Column(Order = 5)]
        public string CourseId { get; set; }

        [DisplayName("課名")]
        [Column(Order = 6)]
        public string CourseName { get; set; }

        [DisplayName("評論日期")]
        [Column(Order = 7)]
        public DateTime LastModified { get; set; }

        [Column(Order = 8)]
        public string CommentId { get; set; }

        [DisplayName("評論內容")]
        [Column(Order = 9)]
        public string CommentString { get; set; }

    }

    public class UserRankingViewModel
    {
        [Key]
        [Column(Order = 1)]
        public int User_RankingId { get; set; }

        [Column(Order = 2)]
        public string UserId { get; set; }

        [DisplayName("暱稱")]
        [Column(Order = 3)]
        public string NickName { get; set; }

        [Column(Order = 4)]
        public string Email { get; set; }

        [Column(Order = 5)]
        public string CourseId { get; set; }

        [DisplayName("課名")]
        [Column(Order = 6)]
        public string CourseName { get; set; }

        [Column(Order = 7)]
        public string RankingId { get; set; }

        [DisplayName("評價日期")]
        [Column(Order = 8)]
        public DateTime LastModified { get; set; }

        [DisplayName("深度")]
        [Column(Order = 9)]
        public int deepness { get; set; }

        [DisplayName("涼度")]
        [Column(Order = 10)]
        public int relaxing { get; set; }

        [DisplayName("甜度")]
        [Column(Order = 11)]
        public int sweetness { get; set; }
    }

    public class UserQuestionViewModel
    {
        [Key]
        public int User_QuestionId { get; set; }
        public string UserId { get; set; }
        [DisplayName("暱稱")]
        public string NickName { get; set; }
        public string Email { get; set; }
        public string CourseId { get; set; }
        [DisplayName("課名")]
        public string CourseName { get; set; }
        public string questionId { get; set; }
        [DisplayName("提問內容")]
        public string questionString { get; set; }
        [DisplayName("提問日期")]
        public DateTime LastModified { get; set; }
        public List<Response> responseData { get; set; }
    }

    public class UserQuestionResponseViewModel
    {
        [Key]
        public int User_Question_ResponseId { get; set; }
        public string userId { get; set; }
        [DisplayName("暱稱")]
        public string NickName { get; set; }
        public string Email { get; set; }

        public string courseId { get; set; }
        [DisplayName("課名")]
        public string CourseName { get; set; }

        public string questionId { get; set; }
        [DisplayName("提問內容")]
        public string questionString { get; set; }

        public string reesponseId { get; set; }
        [DisplayName("回覆內容")]
        public string responseString { get; set; }

        [DisplayName("回覆日期")]
        public DateTime LastModified { get; set; }
        public List<Response> responseData { get; set; }
    }
}