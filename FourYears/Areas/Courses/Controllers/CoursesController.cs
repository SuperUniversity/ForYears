using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using MvcClient.Areas.Courses.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using FourYears.Models;
using FourYears.Areas.Courses.ViewModel;

namespace MvcClient.Areas.Courses.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses/Courses
        public ActionResult Index()
        {
            return View();
        }


        public static String domain = "http://taiwanuniversitiescourses.azurewebsites.net/";
        public static String queryString = null;


        //public async Task<ActionResult> GetNtuBySearchAll(string query = null)
        //{
        //    IEnumerable<NtuCourseModel> ntuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NtuCourseModel>(domain, "ntu", query);
        //    return PartialView(ntuCourses);
        //}


        //public async Task<ActionResult> GetNtpuBySearchAll(string query = null)
        //{
        //    IEnumerable<NtpuCourseModel> ntpuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NtpuCourseModel>(domain, "ntpu", query);
        //    return PartialView(ntpuCourses);
        //}


        //public async Task<ActionResult> GetNctuBySearchAll(string query = null)
        //{
        //    IEnumerable<NctuCourseModel> nctuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NctuCourseModel>(domain, "nctu", query);
        //    return PartialView(nctuCourses);
        //}


        //public async Task<ActionResult> GetNckuBySearchAll(string query = null)
        //{
        //    IEnumerable<NckuCourseModel> nckuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NckuCourseModel>(domain, "ncku", query);
        //    return PartialView(nckuCourses);
        //}

        //[ChildActionOnly]
        public async Task<ActionResult> GetBySearchAll(bool isGeneralSearch=true, string college = "NTU", string query = null)
        {
            //General Search(GetBySearchAll)
            if (isGeneralSearch)
            {
                IEnumerable<AllCollegeCourseModel> AllCollegeCourses = await CoursesControllerUtl.BySearchAllWholeWorkForAll<AllCollegeCourseModel>(domain, college, query);
                return PartialView(AllCollegeCourses);
            }
            //Advanced Search(GetBySearchEach)
            else
            {
                string[] queries = query.Split(',');
                if (queries.Length > 1)
                {
                    query = String.Join("&", queries);
                }

                IEnumerable<AllCollegeCourseModel> AllCollegeCourses = await CoursesControllerUtl.BySearchAllWholeWorkForAllAdv<AllCollegeCourseModel>(domain, college, query);
                return PartialView(AllCollegeCourses);
            }
        }



        public async Task<ActionResult> GetCourseDetail(string strid)
        {
            AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", strid);

            if (SingleCourse != null)
            {


                //Daeal withh ranking
                if(SingleCourse.rankingdata !=null && SingleCourse.rankingdata.Count()!=0)
                {
                    List<Ranking> rankings = SingleCourse.rankingdata;
                    List<double> AvgRankings = CoursesControllerUtl.AvgRankings(rankings);

                    ViewBag.rankingLen = AvgRankings[0];
                    ViewBag.avgDeepness = string.Format("{0:0.00}", AvgRankings[1]);
                    ViewBag.avgRelaxing = string.Format("{0:0.00}", AvgRankings[2]);
                    ViewBag.avgSweetness = string.Format("{0:0.00}", AvgRankings[3]);
                    var rankedIds = rankings.Select(r => r.userID).ToList();
                    ViewBag.HaveNotRanked = (!rankedIds.Contains(User.Identity.GetUserId())).ToString().ToLower();
                }
                else
                {
                    ViewBag.rankingLen = 0;
                    ViewBag.avgDeepness = "尚無評價資料";
                    ViewBag.avgRelaxing = "尚無評價資料";
                    ViewBag.avgSweetness = "尚無評價資料";
                    ViewBag.HaveNotRanked = "true";
                }


                //Daeal withh comment
                if (SingleCourse.commentdata == null || SingleCourse.commentdata.Count() == 0)
                {
                    SingleCourse.commentdata = CoursesControllerUtl.generateFirstManagerComment();
                }
                else
                {
                    List<Comment> commentData = SingleCourse.commentdata;
                    foreach (Comment comment in commentData)
                    {
                        if (comment.anonym)
                        {
                            comment.name = "匿名評論";
                        }
                    }
                }

                //Daeal withh question
                if (SingleCourse.questiondata == null || SingleCourse.commentdata.Count() == 0)
                {
                    ViewBag.originalQuestionLen = 0;
                    SingleCourse.questiondata = CoursesControllerUtl.generateFirstManagerQuestion();
                }
                else
                {
                    List<Question> questionData = SingleCourse.questiondata;
                    foreach (Question question in questionData)
                    {
                        if (question.anonym)
                        {
                            question.name = "匿名提問";
                        }
                        if (question.responsedata != null)
                        {
                            foreach (Response response in question.responsedata)
                            {
                                if (response.anonym)
                                {
                                    response.name = "匿名回覆";
                                }
                            }
                            question.responsedata = (from r in question.responsedata orderby r.lastModified descending select r).ToList();
                        }
                    }
                }

                SingleCourse.commentdata = (from c in SingleCourse.commentdata orderby c.lastModified descending select c).ToList();
                SingleCourse.questiondata = (from q in SingleCourse.questiondata orderby q.lastModified descending select q).ToList();
            }


            else
            {
                ViewBag.rankingLen = 0;
                ViewBag.avgDeepness = "尚無評價資料";
                ViewBag.avgRelaxing = "尚無評價資料";
                ViewBag.avgSweetness = "尚無評價資料";
                ViewBag.HaveNotRanked = "true";
                SingleCourse = new AllCollegeCourseModel()
                {
                    commentdata = CoursesControllerUtl.generateFirstManagerComment(),
                    questiondata = CoursesControllerUtl.generateFirstManagerQuestion()
                };
            }

            ViewBag.questionLen = SingleCourse.questiondata.Count();
            ViewBag.commentLen = SingleCourse.commentdata.Count();
            ApplicationDbContext db = new ApplicationDbContext();
            bool haveFavorited = (from f in db.userFavorites where f.CourseId == SingleCourse._id select f.UserId).Contains(User.Identity.GetUserId() );
            ViewBag.userFavorite = haveFavorited;

            return View(SingleCourse);

        }
        

        public ActionResult GetComments(List<Comment> commentData)
        {
            return PartialView(commentData);
        }

        public ActionResult GetQuestions(List<Question> questionData)
        {
            return PartialView(questionData);
        }

        public ActionResult GetResponses(List<Response> responseData)
        {
            return PartialView(responseData);
        }

        public async Task<ActionResult> GetTeacherCoursesComparason(string college, string teacherName)
        {
            IEnumerable<AllCollegeCourseModel> ComparasonCourses = await CoursesControllerUtl.BySearchAllWholeWorkForAllAdv<AllCollegeCourseModel>(domain, college, "teachername="+ teacherName);

            return PartialView("GetBySearchAll", ComparasonCourses);
        }

        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult PostComment(User_Comment userComment)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            db.userComments.Add(userComment);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail",new { strid = userComment.CourseId});
        }


        [HttpPost]
        public ActionResult PostRanking(User_Ranking userRanking)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            db.userRankings.Add(userRanking);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail", new {strid = userRanking.CourseId });
        }

        [HttpPost]
        public ActionResult PostQuestion(User_Question userQuestion)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.userQuestions.Add(userQuestion);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail", new { strid = userQuestion.CourseId });
        }

        [HttpPost]
        public ActionResult PostResponse(User_Question_Response userQuestionResponse)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            db.userQuestionResponses.Add(userQuestionResponse);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail", new { strid = userQuestionResponse.courseId });
        }

        [HttpPost]
        public ActionResult PostFavorite(User_Favorite userFavorite)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            db.userFavorites.Add(userFavorite);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail", new { strid = userFavorite.CourseId });
        }

        [HttpPost]
        public ActionResult DeleteFavorite(User_Favorite userFavorite)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            User_Favorite toboDeleteUser_Favorite = db.userFavorites.Where(f => f.UserId == userFavorite.UserId).Where(f => f.CourseId == userFavorite.CourseId).FirstOrDefault();
            db.userFavorites.Remove(toboDeleteUser_Favorite);
            db.SaveChanges();
            return RedirectToAction("GetCourseDetail", new { strid = userFavorite.CourseId });
        }




        public ActionResult News()
        {
            ViewBag.QuestionCount = db.userQuestions.Where(q => q.CourseId != "5941d2207bd812e6918ce7f9").ToList().Count;
            ViewBag.CommentCount = db.userComments.Where(q => q.CourseId != "5941d2207bd812e6918ce7f9").ToList().Count;
            ViewBag.RankingCount = db.userRankings.Where(q => q.CourseId != "5941d2207bd812e6918ce7f9").ToList().Count;

            return View();
        }

        public async Task<ActionResult> GetTopTenQuestions()
        {
            //NewQuestions
            Dictionary<string, AllCollegeCourseModel> courseQuestionDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var question in db.userQuestions)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", question.CourseId);
                courseQuestionDict.Add(question.questionId, SingleCourse);
            }

            var userQuestionViewModel = (from q in courseQuestionDict
                                         where q.Value._id != "5941d2207bd812e6918ce7f9"  //測試用課程
                                         select new UserQuestionViewModel
                                         {
                                             CourseId = q.Value._id,
                                             CourseName = q.Value.CourseName,
                                             questionString = q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().questionstring,
                                             responseData = (q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().responsedata != null)? q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().responsedata.OrderByDescending(nq => nq.lastModified).Take(10).ToList() : null,
                                             LastModified = q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().lastModified
                                         }).OrderByDescending(nq => nq.LastModified).Take(10).ToList();

            return PartialView(userQuestionViewModel);
        }

        public ActionResult GetTopTenResponses(List<Response> responses)
        {
            return PartialView(responses);
        }

        public async Task<ActionResult> GetTopTenComments()
        {
            //NewComment
            Dictionary<string, AllCollegeCourseModel> courseCommentDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var comment in db.userComments)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", comment.CourseId);
                courseCommentDict.Add(comment.CommentId, SingleCourse);
            }

            var NewComments = (from c in courseCommentDict
                               where c.Value._id != "5941d2207bd812e6918ce7f9"  //測試用課程
                               select new UserCommentViewModel
                               {
                                   CourseId = c.Value._id,
                                   CourseName = c.Value.CourseName,
                                   CommentString = c.Value.commentdata.Where(cc => cc.commentID == c.Key).FirstOrDefault().commentstring,
                                   LastModified = c.Value.commentdata.Where(cc => cc.commentID == c.Key).FirstOrDefault().lastModified
                               }).OrderByDescending(nc => nc.LastModified).Take(10).ToList();

            return PartialView(NewComments);
        }

        public async Task<ActionResult> GetTopTenRankings()
        {
            //NewRanking
            Dictionary<string, AllCollegeCourseModel> courseRankingDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var ranking in db.userRankings)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", ranking.CourseId);
                courseRankingDict.Add(ranking.RankingId, SingleCourse);
            }

            var NewRankings = (from r in courseRankingDict
                           where r.Value._id != "5941d2207bd812e6918ce7f9"  //測試用課程
                           select new UserRankingViewModel
                           {
                               CourseId = r.Value._id,
                               CourseName = r.Value.CourseName,
                               deepness = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().deepness,
                               relaxing = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().relaxing,
                               sweetness = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().sweetness,
                               LastModified = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().lastModified
                           }).OrderByDescending(nc => nc.LastModified).Take(10).ToList();
            return PartialView(NewRankings);
        }



        [Authorize]
        public ActionResult GetPersonalFootPrint()
        {
            string currentUserId = User.Identity.GetUserId();

            ViewBag.FavoriteCount= db.userFavorites.Where(q => q.UserId == currentUserId).ToList().Count;
            ViewBag.QuestionCount = db.userQuestions.Where(q => q.UserId == currentUserId).ToList().Count;
            ViewBag.ResponseCount = db.userQuestionResponses.Where(q => q.userId == currentUserId).ToList().Count;
            ViewBag.CommentCount = db.userComments.Where(q => q.UserId == currentUserId).ToList().Count;
            ViewBag.RankingCount = db.userRankings.Where(q => q.UserId == currentUserId).ToList().Count;

            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetPersonalFavorite()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            List<User_Favorite> UserFavorites = db.userFavorites.Where(c => c.UserId == currentUserId).ToList();

            List<AllCollegeCourseModel> courses = new List<AllCollegeCourseModel>();
            foreach (var userfavorite in UserFavorites)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", userfavorite.CourseId);
                courses.Add(SingleCourse);
            }

            return PartialView("GetBySearchAll", courses);
        }

        [Authorize]
        public async Task<ActionResult> GetPersonalQuestion()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            List<User_Question> UserQuestions = db.userQuestions.Where(c => c.UserId == currentUserId).ToList();

            Dictionary<string, AllCollegeCourseModel> coursesDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var userquestion in UserQuestions)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", userquestion.CourseId);
                coursesDict.Add(userquestion.questionId, SingleCourse);
            }

            var Questions = (from q in coursesDict
                                         select new UserQuestionViewModel
                                         {
                                             CourseId = q.Value._id,
                                             CourseName = q.Value.CourseName,
                                             questionString = q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().questionstring,
                                             responseData = q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().responsedata.OrderByDescending(nq => nq.lastModified).Take(10).ToList(),
                                             LastModified = q.Value.questiondata.Where(qq => qq.questionID == q.Key).FirstOrDefault().lastModified
                                         }).OrderByDescending(vm => vm.LastModified).ToList();

            return PartialView("GetTopTenQuestions", Questions);
        }


        [Authorize]
        public async Task<ActionResult> GetPersonalResponse()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            List<User_Question_Response> UserResponses = db.userQuestionResponses.Where(c => c.userId == currentUserId).ToList();


            Dictionary<string, AllCollegeCourseModel> coursesResponseIdDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var userresponse in UserResponses)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", userresponse.courseId);
                coursesResponseIdDict.Add(userresponse.questionId + "/" + userresponse.reesponseId, SingleCourse);
            }
            
            var Responses = (from r in coursesResponseIdDict
                             select new UserQuestionResponseViewModel
                             {
                                 courseId = r.Value._id,
                                 CourseName = r.Value.CourseName,
                                 questionString = r.Value.questiondata.Where(rr => rr.questionID == r.Key.Split('/')[0]).FirstOrDefault().questionstring,
                                 responseString = r.Value.questiondata.Where(rr => rr.questionID == r.Key.Split('/')[0]).FirstOrDefault()
                                                    .responsedata.Where(rr => rr.responseID == r.Key.Split('/')[1]).FirstOrDefault().responsestring,
                                 LastModified = r.Value.questiondata.Where(rr => rr.questionID == r.Key.Split('/')[0]).FirstOrDefault()
                                                    .responsedata.Where(rr => rr.responseID == r.Key.Split('/')[1]).FirstOrDefault().lastModified
                             }).OrderByDescending(vm => vm.LastModified).ToList();

            return PartialView("GetTopTenResponses_Personal", Responses);
        }


        [Authorize]
        public async Task<ActionResult> GetPersonalComment()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            List<User_Comment> UserComments = db.userComments.Where(c => c.UserId == currentUserId).ToList();

            Dictionary<string, AllCollegeCourseModel> coursesDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var usercomment in UserComments)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", usercomment.CourseId);
                coursesDict.Add(usercomment.CommentId, SingleCourse);
            }
            var Comments = (from c in coursesDict
                               select new UserCommentViewModel
                               {
                                   CourseId = c.Value._id,
                                   CourseName = c.Value.CourseName,
                                   CommentString = c.Value.commentdata.Where(cc => cc.commentID == c.Key).FirstOrDefault().commentstring,
                                   LastModified = c.Value.commentdata.Where(cc => cc.commentID == c.Key).FirstOrDefault().lastModified
                               }).OrderByDescending(vm => vm.LastModified).ToList();

            return PartialView("GetTopTenComments", Comments);
        }

        [Authorize]
        public async Task<ActionResult> GetPersonalRanking()
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            List<User_Ranking> UserRanking = db.userRankings.Where(c => c.UserId == currentUserId).ToList();

            Dictionary<string, AllCollegeCourseModel> coursesDict = new Dictionary<string, AllCollegeCourseModel>();
            foreach (var userranking in UserRanking)
            {
                AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", userranking.CourseId);
                coursesDict.Add(userranking.RankingId, SingleCourse);
            }

            var Rankings = (from r in coursesDict
                               select new UserRankingViewModel
                               {
                                   CourseId = r.Value._id,
                                   CourseName = r.Value.CourseName,
                                   deepness = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().deepness,
                                   relaxing = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().relaxing,
                                   sweetness = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().sweetness,
                                   LastModified = r.Value.rankingdata.Where(rr => rr.rankingID == r.Key).FirstOrDefault().lastModified
                               }).OrderByDescending(vm => vm.LastModified).Take(10).ToList();

            return PartialView("GetTopTenRankings", Rankings);
        }

        //[HttpPost]
        //public async Task<ActionResult> PostComment(string strid, Comment comment)
        //{
        //    AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", strid);
        //    return PartialView(SingleCourse);
        //}



        //[HttpPost]
        //public async Task<ActionResult> PostRanking(string strid, Ranking ranking)
        //{
        //    //TODO
        //    //string address = "http://taiwanuniversitiescourses.azurewebsites.net/api/AllCollege?strid=" + strid + "&isranking=true";
        //    queryString = "api/AllCollege?strid=" + strid + "&isranking=true";
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(domain);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        //GET Method  
        //        HttpResponseMessage response = await client.PutAsJsonAsync(queryString, ranking);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //return View();
        //            ViewBag.Message = "新增成功";
        //            RedirectToAction("GetCourseDetail");
        //        }   
        //        else
        //        {
        //            ViewBag.Message = "新增失敗";
        //        }
        //    }

        //    return View();
        //    //AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", strid);
        //    //return PartialView(SingleCourse);
        //}




    }
}