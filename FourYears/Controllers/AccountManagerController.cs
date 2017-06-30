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


        // GET: AccountManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: AccountManager/Edit/5
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

        // GET: AccountManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
