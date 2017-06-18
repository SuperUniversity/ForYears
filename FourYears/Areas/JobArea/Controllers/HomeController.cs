using FourYears.Areas.JobArea.Models;
using FourYears.Areas.JobArea.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FourYears.Areas.JobArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: JobArea/Home
        superuniversityEntities1 db = new superuniversityEntities1();
        IRepository<Job> job = new Repository<Job>();
        IRepository<EmployerCompany> _Emp = new Repository<EmployerCompany>();
        // GET: JobArea/Home
        public ActionResult Index(int page = 1, int perpage = 15, string sortby = "", string Jobnamesreach = "", string Workplacesreach = "", string Allsreach = "")
        {
            //前台的工作管理

            //收尋的block            
            var jobfront = new List<Job>();
            var list = db.Job.ToList();
            string allstr = string.IsNullOrEmpty(Allsreach) ? "" : Allsreach;
            string jobstr = string.IsNullOrEmpty(Jobnamesreach) ? "" : Jobnamesreach;
            string workstr = string.IsNullOrEmpty(Workplacesreach) ? "" : Workplacesreach;

            if (allstr == "" && jobstr == "" && workstr == "")
            {
                jobfront = list;
            }
            else if (allstr != "")
            {
                jobfront = list.Select(p => p).Where(p => p.JobName.Contains(allstr) || p.Workplace.Contains(allstr)).ToList();
            }
            else
            {
                jobfront = list.Select(p => p).Where(p => p.JobName.Contains(jobstr)).ToList()
                  .Union(list.Select(p => p).Where(p => p.Workplace.Contains(workstr))).ToList();
            }

            ViewBag.SreachbyName = jobstr;
            ViewBag.SreachbyPlace = workstr;
            ViewBag.SreachbyAll = allstr;
            //排列的block
            ViewBag.SortbyID = string.IsNullOrEmpty(sortby) ? "JobID desc" : "";
            ViewBag.SortbyName = (sortby == "JobName") ? "JobName desc" : "JobName";
            ViewBag.SortbyPlace = (sortby == "Workplace") ? "Workplace desc" : "Workplace";
            ViewBag.SortbyPay = (sortby == "PayPerHour") ? "PayPerHour desc" : "PayPerHour";

            switch (sortby)
            {
                case "JobID desc":
                    jobfront = jobfront.OrderByDescending(p => p.JobID).ToList();
                    break;
                case "JobName desc":
                    jobfront = jobfront.OrderByDescending(p => p.JobName).ToList();
                    break;
                case "JobName":
                    jobfront = jobfront.OrderBy(p => p.JobName).ToList();
                    break;
                case "Workplace desc":
                    jobfront = jobfront.OrderByDescending(p => p.Workplace).ToList();
                    break;
                case "Workplace":
                    jobfront = jobfront.OrderBy(p => p.Workplace).ToList();
                    break;
                case "PayPerHour desc":
                    jobfront = jobfront.OrderByDescending(p => p.PayPerHour).ToList();
                    break;
                case "PayPerHour":
                    jobfront = jobfront.OrderBy(p => p.PayPerHour).ToList();
                    break;
                default:
                    jobfront = jobfront.OrderBy(p => p.JobID).ToList();
                    break;

            }


            return View(jobfront.ToList().ToPagedList(page, perpage));
        }

        public ActionResult Detail(Job j, int id)
        {
            CompanyName cn = new CompanyName(id);
            var result = (from s in db.Job
                          where s.JobID == id
                          select s).FirstOrDefault();
            if (result == default(Models.Job))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.id = id;
                ViewBag.name = cn;
                ViewBag.map = db.Job.Find(id).Workplace.ToString();
                ViewBag.datas = db.Jobtime.ToList();
                return View(job.GetById(id));

            }

        }
        [HttpPost]
        public ActionResult AddComment(int id, string Content)
        {
            var userid = HttpUtility.UrlDecode(Request.Cookies["name"].Value);
            var now = DateTime.Now;
            var comment = new Models.JobCommet()
            {
                JobID = id,
                Content = Content,
                UserID = userid,
                CreateDate = now
            };
            using (Models.superuniversityEntities1 db = new Models.superuniversityEntities1())
            {
                db.JobCommets.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Detail", new { id = id });

        }
        public ActionResult CompanyDetail(EmployerCompany emp, int id)
        {
            JobNameID jnd = new JobNameID(id);
            ViewBag.Gmap = db.EmployerCompany.Find(job.GetById(id).CompanyID).CompanyAdress.ToString();
            ViewBag.job = jnd;

            return View(_Emp.GetById(job.GetById(id).CompanyID));
        }
        public ActionResult Hire(int id)
        {
            JobNameID jnd = new JobNameID(id);
            ViewBag.job = jnd;
            return View();
        }
        [HttpPost]
        public ActionResult Hire(Job j, int id, string[] nothing)
        {
            var receviceemail = nothing[1];
            JobNameID jnd = new JobNameID(id);
            ViewBag.job = jnd;

            if (receviceemail != null && receviceemail != "")
            {
                System.Net.Mail.MailMessage em = new System.Net.Mail.MailMessage();
                em.From = new System.Net.Mail.MailAddress("rayho880058@gmail.com", "MSIT11403人資部", System.Text.Encoding.UTF8);
                em.To.Add(new System.Net.Mail.MailAddress(string.Format("{0}", receviceemail.ToString())));    //收件者
                em.Subject = string.Format("你主動應徵{0}已被讀取", job.GetById(id).JobName);     //信件主題 
                em.SubjectEncoding = System.Text.Encoding.UTF8;
                em.Body = string.Format("{0}你好,你於{1}應徵{2}的信函已被讀取，由於人事單位在處理眾多求職者資料時需要一些時間，若您的資格符合他們的需求將會立即通知您面試，請靜候佳音，謝謝您！<p>(注意：本讀取回條僅代表廠商已打開您的履歷資料，不代表錄取通知！)</p><p>此信件為系統自動發送，請勿直接回覆。</p>", HttpUtility.UrlDecode(Request.Cookies["name"].Value), DateTime.Now, job.GetById(id).JobName);            //內容 
                em.BodyEncoding = System.Text.Encoding.UTF8;
                em.IsBodyHtml = true;     //信件內容是否使用HTML格式
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                //登入帳號認證  
                smtp.Credentials = new System.Net.NetworkCredential("rayho880058@gmail.com", "some137root724");
                //使用587 Port - google要設定
                smtp.Port = 587;
                smtp.EnableSsl = true;   //啟動SSL 
                                         //end of google設定
                smtp.Host = "smtp.gmail.com";   //SMTP伺服器
                smtp.Send(em);

                TempData["message"] = "已寄出申請";


                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "輸入錯誤";
                return View();

            }


        }
    }
}