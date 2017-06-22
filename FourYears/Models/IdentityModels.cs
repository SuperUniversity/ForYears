using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace FourYears.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
    }


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AzureSuperUniversity", throwIfV1Schema: false)
        {
        }

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

        public static List<ApplicationUser> GetAll()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return db.Users.ToList();
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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}