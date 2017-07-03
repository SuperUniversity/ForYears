using MongoDB.Driver;
using MvcClient.Areas.Courses.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace MvcClient.Areas.Courses.Controllers
{
    public class CoursesControllerUtl
    {
        public static string QueryStringGenerator(string ApiUniversity, string query, int topn=-1)
        {
            string queryString = @"api/" + ApiUniversity;


            if (query == null)
            {
                if (topn != -1)
                {
                    queryString += "?topn=" + topn;
                }
            }
            else
            {
                if (query.Contains("-"))
                {
                    string queries = query.Split('-')[0];
                    string exclude = query.Split('-')[1];
                    queryString += splitquery(queries);
                    queryString += "&exclude=" + exclude;
                }
                else
                {
                    queryString += splitquery(query);
                }

                if (topn != -1)
                {
                    queryString += "&topn=" + topn;
                }
            }
            return queryString;
        }

        public static string splitquery(string queries)
        {
            string queryString = null;
            int queryLen = queries.Split().Count();

            switch (queryLen)
            {
                case 1:
                    queryString = "?query=" + queries;
                    break;
                case 2:
                    queryString = "?query=" + queries.Split()[0] + "&query2=" + queries.Split()[1];
                    break;
                default:
                    queryString = "?query=" + queries.Split()[0] + "&query2=" + queries.Split()[1] + "&query3=" + queries.Split()[2];
                    break;
            }
            return queryString;
        }


        public static async Task<IEnumerable<T>> GetFromApi<T>(string domain,string queryString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                HttpResponseMessage response = await client.GetAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<T> targetCourses = await response.Content.ReadAsAsync<List<T>>();
                    return targetCourses;
                }
                else
                {
                    return null;
                }
            }

        }

        public static async Task<IEnumerable<T>> BySearchAllWholeWork<T>(string domain, string ApiUniversity,string query)
        {
            IEnumerable<T> Courses = null;
            string queryString = null;
            if (query == null)
            {
                queryString = CoursesControllerUtl.QueryStringGenerator(ApiUniversity, query, 100);
                Courses = await CoursesControllerUtl.GetFromApi<T>(domain, queryString);
            }
            else
            {
                queryString = CoursesControllerUtl.QueryStringGenerator(ApiUniversity, query);
                Courses = await CoursesControllerUtl.GetFromApi<T>(domain, queryString);
            }

            return Courses;
        }








        //For All Start
        public static string QueryStringGeneratorForAll(string ApiUniversity, string query, string college, int topn = -1)
        {
            string queryString = @"api/" + ApiUniversity + "?college="+college;


            if (query == null)
            {
                if (topn != -1)
                {
                    queryString += "&topn=" + topn;
                }
            }
            else
            {
                if (query.Contains("-"))
                {
                    string queries = query.Split('-')[0];
                    string exclude = query.Split('-')[1];
                    queryString += splitqueryForAll(queries);
                    queryString += "&exclude=" + exclude;
                }
                else
                {
                    queryString += splitqueryForAll(query);
                }

                if (topn != -1)
                {
                    queryString += "&topn=" + topn;
                }
            }
            return queryString;
        }

        public static string splitqueryForAll(string queries)
        {
            string queryString = null;
            int queryLen = queries.Split().Count();

            switch (queryLen)
            {
                case 1:
                    queryString = "&query=" + queries;
                    break;
                case 2:
                    queryString = "&query=" + queries.Split()[0] + "&query2=" + queries.Split()[1];
                    break;
                default:
                    queryString = "&query=" + queries.Split()[0] + "&query2=" + queries.Split()[1] + "&query3=" + queries.Split()[2];
                    break;
            }
            return queryString;
        }


        public static async Task<IEnumerable<T>> GetFromApiForAll<T>(string domain, string queryString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                HttpResponseMessage response = await client.GetAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<T> targetCourses = await response.Content.ReadAsAsync<List<T>>();
                    return targetCourses;
                }
                else
                {
                    return null;
                }
            }
        }

        public static async Task<IEnumerable<T>> BySearchAllWholeWorkForAll<T>(string domain, string college, string query)
        {
            IEnumerable<T> Courses = null;
            string queryString = null;
            if (query == null)
            {
                queryString = CoursesControllerUtl.QueryStringGeneratorForAll("AllCollege", query, college, 100);
                Courses = await CoursesControllerUtl.GetFromApiForAll<T>(domain, queryString);
            }
            else
            {
                queryString = CoursesControllerUtl.QueryStringGeneratorForAll("AllCollege", query, college);
                Courses = await CoursesControllerUtl.GetFromApiForAll<T>(domain, queryString);
            }

            return Courses;
        }

        public static async Task<IEnumerable<T>> BySearchAllWholeWorkForAllAdv<T>(string domain, string college, string query)
        {
            IEnumerable<T> Courses = null;
            string queryString = "api/AllCollege?college=" + college+ "&" + query;
            if (query == null)
            {
                Courses = await CoursesControllerUtl.GetFromApiForAll<T>(domain, queryString);
            }
            else
            {
                Courses = await CoursesControllerUtl.GetFromApiForAll<T>(domain, queryString);
            }

            return Courses;
        }

        public static async Task<T> GetFromApiForAllSingle<T>(string domain, string queryString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                HttpResponseMessage response = await client.GetAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    T targetCourse = await response.Content.ReadAsAsync<T>();
                    return targetCourse;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public static async Task<T> ByIdWholeWorkForAll<T>(string domain, string ApiUniversity, string strid)
        {
            string queryString = @"api/" + ApiUniversity + "?strid=" + strid;
            T Courses = await CoursesControllerUtl.GetFromApiForAllSingle<T>(domain, queryString);

            return Courses;
        }

        public static List<double> AvgRankings(List<Ranking> rankings )
        {
            List<int> deepnessLi = new List<int>();
            List<int> relaxingLi = new List<int>();
            List<int> sweetnessLi = new List<int>();

            double rankingLen = rankings.Count();
            foreach (Ranking ranking in rankings)
            {
                deepnessLi.Add(ranking.deepness);
                relaxingLi.Add(ranking.relaxing);
                sweetnessLi.Add(ranking.sweetness);
            }

            double avgDeepness = deepnessLi.Average();
            double avgRelaxing = relaxingLi.Average();
            double avgSweetness = sweetnessLi.Average();
            List<double> AvgRankings = new List<double>();

            AvgRankings.Add(rankingLen);
            AvgRankings.Add(avgDeepness);
            AvgRankings.Add(avgRelaxing);
            AvgRankings.Add(avgSweetness);

            return AvgRankings;
        }

        public static List<Comment> generateFirstManagerComment()
        {
            Comment comment = new Comment() { name = "管理員", commentstring = "快點成為第一個留言的人吧" };
            comment.lastModified = DateTime.Now;
            List<Comment> comments = new List<Comment>();
            comments.Add(comment);

            return comments;
        }

        public static List<Question> generateFirstManagerQuestion()
        {
            Question question = new Question() { name = "管理員", questionstring = "快點成為第一個提問的人吧" };
            question.lastModified = DateTime.Now;
            List<Question> questions = new List<Question>();
            questions.Add(question);

            return questions;
        }


        public static async Task<string> DeleteFromApiForAll(string domain, string queryString)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //Delete Method  
                HttpResponseMessage response = await client.DeleteAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    return "Success";
                }
                else
                {
                    return "Fail";
                }

            }
        }

    }
}