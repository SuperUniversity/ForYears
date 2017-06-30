using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FourYears.ViewModel;

namespace FourYears.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreateTime { get; set; }
        public bool AllowEmailContact { get; set; }
        public string ActualName { get; set; }
        public string NickName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
        public virtual ICollection<LoginLog> LoginLog { get; set; }
        //public virtual University University { get; set; }
        public int UniversityId { get; set; }
    }

    public class LoginLog
    {
        [Key]
        [Column(Order = 1)]
        public int LogId{ get; set; }
        [Column(Order = 2)]
        public string UserId { get; set; }
        [Column(Order = 3)]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public DateTime LogInTime { get; set; }
    }

    //public System.Data.Entity.DbSet<LoginLog> LoginLogs { get; set; }

    public class University
    {
        [Key]
        [Column(Order = 1)]
        public int UniversityId { get; set; }
        [Column(Order = 2)]
        [StringLength(256)]
        public string EduCode { get; set; }
        [Column(Order = 3)]
        [StringLength(256)]
        public string ChineseName { get; set; }
        [Column(Order = 4)]
        [StringLength(256)]
        public string EnglishName { get; set; }
        [Column(Order = 5)]
        [StringLength(256)]
        public string PostCode { get; set; }
        [Column(Order = 6)]
        [StringLength(256)]
        public string Address { get; set; }
        [Column(Order = 7)]
        [StringLength(256)]
        public string Phone { get; set; }
        [Column(Order = 8)]
        [StringLength(256)]
        public string Fax { get; set; }
        [Column(Order = 9)]
        [StringLength(256)]
        public string Website { get; set; }
        [Column(Order = 10)]
        public DateTime UpdateDate { get; set; }
    }

    //public System.Data.Entity.DbSet<University> University { get; set; }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AzureSuperUniversity", throwIfV1Schema: false)
        {

        }

        public DbSet<University> University { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }

        public static string GetNickName(string id)
        {
            if(id != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser currentUser = db.Users.Find(id);
                
                if (currentUser != null)
                {
                    if (currentUser.NickName == null)
                    {
                        return currentUser.Email;
                    }
                    
                    return currentUser.NickName;
                }
            }
            return null;
        }

        public static ApplicationUser GetUserById(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            return db.Users.Find(Id);
        }

        public static List<ApplicationUser> GetAllUsers()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return db.Users.ToList();
        }

        public static string GetFirstRoleNameByUserId(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string roleName = db.Roles.Find(ApplicationDbContext.GetUserById(Id).Roles.FirstOrDefault().RoleId).Name;
            return roleName;
        }

        public static IdentityRole GetRoleById(string RoleId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Roles.Find(RoleId);
        }


        public static List<IdentityRole> GetAllRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Roles.ToList();
        }


        public static bool GetAllowEmailContact(string id)
        {
            if (id != null)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser currentUser = db.Users.Find(id);

                if (currentUser != null)
                {
                    if (currentUser.NickName == null)
                    {
                        return currentUser.AllowEmailContact;
                    }

                    return currentUser.AllowEmailContact;
                }
            }
           
            return true;
        }

        public static int GetUniversityId(string id)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser currentUser = db.Users.Find(id);

            if (currentUser != null)
            {
                if (currentUser.UniversityId == 0)
                {
                    return 182;
                }
                return currentUser.UniversityId;
            }
            else
            {
                return 182;
            }
        }

        public static DateTime GetCreateTime(string id)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser currentUser = db.Users.Find(id);

            if (currentUser != null)
            {
                return currentUser.CreateTime;
            }
            else
            {
                return DateTime.UtcNow.AddHours(8);
            }
        }

        public static void EditUser(ApplicationUser user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void CreateLoginLog(string UserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser user = db.Users.Find(UserId);
            LoginLog loginLog = new LoginLog();
            loginLog.User = user;
            loginLog.LogInTime = DateTime.UtcNow.AddHours(8);

            if (user.LoginLog.Count() > 0)
            {
                user.LoginLog.Add(loginLog);
            }
            else
            {
                List<LoginLog> newLog = new List<LoginLog>();
                newLog.Add(loginLog);
                user.LoginLog = newLog;
            }
        }


        public static University GetUniversityById(int UniversityId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            University university = db.University.Find(UniversityId);
            return university;
        }

        public static List<University> GetAllUniversities()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.University.ToList();
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<FourYears.ViewModel.AccountManagerViewModel> AccountManagerViewModels { get; set; }

        //public System.Data.Entity.DbSet<FourYears.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<FourYears.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}