using FourYears.Areas.Courses.ViewModel;
using FourYears.Models;
using MvcClient.Areas.Courses.Controllers;
using MvcClient.Areas.Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.Courses.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserCourseController : Controller
    {
        // GET: Courses/UserCourse
        public ActionResult Index()
        {
            return View();
        }



        ApplicationDbContext db = new ApplicationDbContext();
        String domain = "http://taiwanuniversitiescourses.azurewebsites.net/";
        String queryString = null;

        public async Task<ActionResult> DeleteComment(int userCommentId)
        {
            User_Comment userComment =  db.userComments.Find(userCommentId);
            db.userComments.Remove(userComment);
            db.SaveChanges();


            string courseId = userComment.CourseId;
            string commentId = userComment.CommentId;
            queryString = @"api/AllCollege?strid=" + courseId + "&commentId=" + commentId;
            string returnMessage = await CoursesControllerUtl.DeleteFromApiForAll(domain, queryString);

            return RedirectToAction("GetUserComments");
        }

        // GET: Courses/UserCourse/Delete/5
        public async Task<ActionResult> DeleteRanking(int userRankingId)
        {
            User_Ranking userRanking = db.userRankings.Find(userRankingId);
            db.userRankings.Remove(userRanking);
            db.SaveChanges();

            string courseId = userRanking.CourseId;
            string rankingId = userRanking.RankingId;
            queryString = @"api/AllCollege?strid=" + courseId + "&rankingId=" + rankingId;
            string returnMessage = await CoursesControllerUtl.DeleteFromApiForAll(domain, queryString);

            return RedirectToAction("GetUserRankings");
        }

        // GET: Courses/UserCourse
        public async Task<ActionResult> GetUserComments()
        {
            IEnumerable<string> coursesIds = from c in db.userComments.ToList()
                                             select c.CourseId;
            List<AllCollegeCourseModel> CoursesWithComment = new List<AllCollegeCourseModel>();
            foreach (string courseId in coursesIds)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", courseId);
                CoursesWithComment.Add(SingleCourse);
            }
            
            IEnumerable<UserCommentViewModel> cvm = from comment in db.userComments.ToList()
                                                    join course in CoursesWithComment on comment.CourseId equals course._id
                                                    select new UserCommentViewModel
                                                    {
                                                        User_CommentId = comment.User_CommentId,
                                                        UserId = comment.UserId,
                                                        NickName = ApplicationDbContext.GetUserById(comment.UserId).NickName,
                                                        Email = ApplicationDbContext.GetUserById(comment.UserId).UserName,
                                                        CourseId = comment.CourseId,
                                                        //CourseName = CoursesWithComment.Where(c => c._id == comment.CourseId).FirstOrDefault().CourseName,
                                                        //LastModified = CoursesWithComment.Where(c => c._id == comment.CourseId).FirstOrDefault().commentdata.Where(c => c.commentID == comment.CommentId).FirstOrDefault().lastModified,
                                                        //CommentId = comment.CommentId,
                                                        //CommentString = CoursesWithComment.Where(c => c._id == comment.CourseId).FirstOrDefault().commentdata.Where(c => c.commentID == comment.CommentId).FirstOrDefault().commentstring

                                                        CourseName = course.CourseName,
                                                        LastModified = course.commentdata.Where(c => c.commentID == comment.CommentId).FirstOrDefault().lastModified,
                                                        CommentId = comment.CommentId,
                                                        CommentString = course.commentdata.Where(c => c.commentID == comment.CommentId).FirstOrDefault().commentstring
                                                    };
            return View(cvm);
        }

        public async Task<ActionResult> GetUserRankings()
        {

            IEnumerable<string> coursesIds = from c in db.userRankings.ToList()
                                             select c.CourseId;
            List<AllCollegeCourseModel> CoursesWithRanking = new List<AllCollegeCourseModel>();
            foreach (string courseId in coursesIds)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", courseId);
                CoursesWithRanking.Add(SingleCourse);
            }

            IEnumerable<UserRankingViewModel> rvm = from ranking in db.userRankings.ToList()
                                                    join course in CoursesWithRanking on ranking.CourseId equals course._id
                                                    select new UserRankingViewModel
                                                    {
                                                        User_RankingId = ranking.User_RankingId,
                                                        UserId = ranking.UserId,
                                                        NickName = ApplicationDbContext.GetUserById(ranking.UserId).NickName,
                                                        Email = ApplicationDbContext.GetUserById(ranking.UserId).UserName,
                                                        CourseId = ranking.CourseId,
                                                        CourseName = course.CourseName,
                                                        LastModified = course.rankingdata.Where(c => c.rankingID == ranking.RankingId).FirstOrDefault().lastModified,
                                                        deepness = course.rankingdata.Where(c => c.rankingID == ranking.RankingId).FirstOrDefault().deepness,
                                                        sweetness = course.rankingdata.Where(c => c.rankingID == ranking.RankingId).FirstOrDefault().sweetness,
                                                        relaxing = course.rankingdata.Where(c => c.rankingID == ranking.RankingId).FirstOrDefault().relaxing
                                                    };
            //UserRankingViewModel
            return View(rvm);
        }


        //// GET: Courses/UserCourse/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Courses/UserCourse/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Courses/UserCourse/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Courses/UserCourse/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Courses/UserCourse/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        //// POST: Courses/UserCourse/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
