using FourYears.Models;
using FourYears.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FourYears.Controllers
{
    public class AccountManagerController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: AccountManager
        public ActionResult Index()
        {
            //User.IsInRole("Customer");
            List<University> universities = ApplicationDbContext.GetAllUniversities();
            IQueryable<AccountManagerViewModel> users = (from u in ApplicationDbContext.GetAllUsers()
                                                         select new AccountManagerViewModel
                                                         {
                                                             Id = u.Id,
                                                             UserName = u.UserName,
                                                             NickName = u.NickName,
                                                             EmailConfirmed = u.EmailConfirmed,
                                                             AllowEmailContact = u.AllowEmailContact,
                                                             AccessFailedCount = u.AccessFailedCount,
                                                             CreateTime = u.CreateTime,

                                                             UniversityId = u.UniversityId,
                                                             UniversitySelectList = (from uni in universities
                                                                                     select new SelectListItem
                                                                                     {
                                                                                         Text = uni.ChineseName,
                                                                                         Value = uni.UniversityId.ToString(),
                                                                                         Selected = (u.UniversityId == uni.UniversityId) ? true : false
                                                                                     }),

                                                             RoleId = ApplicationDbContext.GetUserById(u.Id).Roles.FirstOrDefault().RoleId,
                                                             RoleSelectList = (from r in ApplicationDbContext.GetAllRoles()
                                                                               select new SelectListItem
                                                                               {
                                                                                   Text = r.Name,
                                                                                   Value = r.Id.ToString(),
                                                                                   Selected = (ApplicationDbContext.GetFirstRoleNameByUserId(u.Id)== r.Name)? true: false
                                                                               })
                                                         }).AsQueryable();
            

            return View(users);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Edit(AccountManagerViewModel user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser EditUser = db.Users.Find(user.Id);

            string OriginalRoleId = EditUser.Roles.FirstOrDefault().RoleId;
            string ChangedRoleId = user.RoleId;
            if (OriginalRoleId != ChangedRoleId)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                UserManager.RemoveFromRole(user.Id, ApplicationDbContext.GetRoleById(OriginalRoleId).Name);
                UserManager.AddToRole(user.Id, ApplicationDbContext.GetRoleById(ChangedRoleId).Name);
            }

            //var Entry = db.Entry(EditUser);
            //Entry.Property(u => u.NickName).IsModified = (EditUser.NickName != user.NickName)? true : false;
            //Entry.Property(u => u.EmailConfirmed).IsModified = (EditUser.EmailConfirmed != user.EmailConfirmed) ? true : false;
            //Entry.Property(u => u.AllowEmailContact).IsModified = (EditUser.AllowEmailContact != user.AllowEmailContact) ? true : false;
            //Entry.Property(u => u.CreateTime).IsModified = (EditUser.NickName != user.NickName)? true : false;
            //Entry.Property(u => u.UniversityId).IsModified = (EditUser.NickName != user.NickName)? true : false;

            EditUser.NickName = user.NickName;
            EditUser.EmailConfirmed = user.EmailConfirmed;
            EditUser.AllowEmailContact = user.AllowEmailContact;
            EditUser.CreateTime = user.CreateTime;
            EditUser.UniversityId = user.UniversityId;
            db.Entry(EditUser).State = EntityState.Modified;
            db.SaveChanges();
            //RedirectToAction("Index");
            //return Action("Index");
        }


        // LoginLog
        [Authorize(Roles = "Admin")]
        public ActionResult GetLoginLogs()
        {
            UserLoginLogViewModel lvm = new UserLoginLogViewModel();
            var GetLoginLogs = from l in ApplicationDbContext.GetAllLoginLog()
                               select new UserLoginLogViewModel
                               {
                                   LogId = l.LogId,
                                   UserId = l.UserId,
                                   NickName = ApplicationDbContext.GetNickName(l.UserId),
                                   Role = ApplicationDbContext.GetRoleById(ApplicationDbContext.GetUserById(l.UserId).Roles.FirstOrDefault().RoleId).Name,
                                   Email = ApplicationDbContext.GetUserById(l.UserId).Email,
                                   loginTime = l.LogInTime
                               };
            return View(GetLoginLogs);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteLoginLog(string LogId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            LoginLog log = db.LoginLogs.Find(int.Parse(LogId));
            db.LoginLogs.Remove(log);
            db.SaveChanges();
            return RedirectToAction("GetLoginLogs");
        }

    }
}
