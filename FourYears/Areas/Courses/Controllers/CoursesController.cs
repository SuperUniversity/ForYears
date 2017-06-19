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


        public async Task<ActionResult> GetNtuBySearchAll(string query = null)
        {
            IEnumerable<NtuCourseModel> ntuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NtuCourseModel>(domain, "ntu", query);
            return PartialView(ntuCourses);
        }


        public async Task<ActionResult> GetNtpuBySearchAll(string query = null)
        {
            IEnumerable<NtpuCourseModel> ntpuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NtpuCourseModel>(domain, "ntpu", query);
            return PartialView(ntpuCourses);
        }


        public async Task<ActionResult> GetNctuBySearchAll(string query = null)
        {
            IEnumerable<NctuCourseModel> nctuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NctuCourseModel>(domain, "nctu", query);
            return PartialView(nctuCourses);
        }


        public async Task<ActionResult> GetNckuBySearchAll(string query = null)
        {
            IEnumerable<NckuCourseModel> nckuCourses = await CoursesControllerUtl.BySearchAllWholeWork<NckuCourseModel>(domain, "ncku", query);
            return PartialView(nckuCourses);
        }

        //[ChildActionOnly]
        public async Task<ActionResult> GetBySearchAll(bool isGeneralSearch=true, string college = "NTU", string query = null)
        {
            if (isGeneralSearch)
            {
                IEnumerable<AllCollegeCourseModel> AllCollegeCourses = await CoursesControllerUtl.BySearchAllWholeWorkForAll<AllCollegeCourseModel>(domain, college, query);
                return PartialView(AllCollegeCourses);
            }
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
                List<Ranking> rankings = SingleCourse.rankingdata;
                List<double> AvgRankings = CoursesControllerUtl.AvgRankings(rankings);

                ViewBag.rankingLen = AvgRankings[0];
                ViewBag.avgDeepness = string.Format("{0:0.00}", AvgRankings[1]);
                ViewBag.avgRelaxing = string.Format("{0:0.00}", AvgRankings[2]);
                ViewBag.avgSweetness = string.Format("{0:0.00}", AvgRankings[3]);

                if (SingleCourse.commentdata == null)
                {
                    SingleCourse.commentdata = CoursesControllerUtl.generateFirstManagerComment(); ;
                }

                SingleCourse.commentdata = (from c in SingleCourse.commentdata orderby c.lastModified descending select c).ToList();

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
                SingleCourse = new AllCollegeCourseModel() { commentdata = CoursesControllerUtl.generateFirstManagerComment() };
            }
            return View(SingleCourse);

        }

        //public async Task<ActionResult> GetComments(string strid)
        //{
        //    List<Comment> comments = null;
        //    try
        //    {
        //        AllCollegeCourseModel SingleCourse = await CoursesControllerUtl.ByIdWholeWorkForAll<AllCollegeCourseModel>(domain, "AllCollege", strid);
        //        comments = SingleCourse.commentdata;
        //        foreach(Comment comment in comments)
        //        {
        //            if (comment.anonym)
        //            {
        //                comment.name = "匿名評論";
        //            }
        //        }

        //        comments = (from c in comments
        //                     orderby c.lastModified descending
        //                     select c).ToList();
        //        //comments.Sort()

        //        ViewBag.commentLen = comments.Count();
        //    }
        //    catch
        //    {
        //        Comment comment = new Comment() {name="管理員", commentstring="快點成為第一個留言的人吧" };
        //        comment.name = "管理員";
        //        comment.lastModified = DateTime.Now;
        //        comments = new List<Comment>();
        //        comments.Add(comment);
        //        ViewBag.commentLen = 0;
        //    }

        //    return PartialView(comments);
        //}

        public ActionResult GetComments(List<Comment> commentData)
        {
            return PartialView(commentData);
        }


        [HttpGet]
        public ActionResult PostComment()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult PostRanking()
        {
            return PartialView();
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