namespace FourYears.Migrations
{
    using FourYears.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FourYears.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FourYears.Models.ApplicationDbContext";
        }

        protected override void Seed(FourYears.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //https://www.youtube.com/watch?v=JMPSahODM8s
            //context.Roles.AddOrUpdate(r => r.Name,
            //    new IdentityRole { Name = "Admin" },
            //    new IdentityRole { Name = "Customer" }
            //    );


            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //UserManager.RemoveFromRole("020e4ea3-75a4-4773-8860-9336271f2e2d", "Customer");
            //UserManager.RemoveFromRole("615cdaf9-a75c-4367-86b6-b22258fd494c", "Customer");
            //UserManager.RemoveFromRole("5bd5cc4e-5364-4814-8280-3381aad6a478", "Customer");

            //UserManager.AddToRole("020e4ea3-75a4-4773-8860-9336271f2e2d", "Admin");
            //UserManager.AddToRole("615cdaf9-a75c-4367-86b6-b22258fd494c", "Admin");
            //UserManager.AddToRole("5bd5cc4e-5364-4814-8280-3381aad6a478", "Admin");

            //foreach (var user in ApplicationDbContext.GetAll())
            //{
            //    UserManager.AddToRole(user.Id, "Customer");
            //}

            
        }
    }
}
