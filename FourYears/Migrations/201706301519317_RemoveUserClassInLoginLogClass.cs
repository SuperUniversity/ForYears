namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserClassInLoginLogClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoginLogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.LoginLogs", new[] { "UserId" });
            AddColumn("dbo.LoginLogs", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.LoginLogs", "UserId", c => c.String());
            CreateIndex("dbo.LoginLogs", "ApplicationUser_Id");
            AddForeignKey("dbo.LoginLogs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginLogs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.LoginLogs", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.LoginLogs", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.LoginLogs", "ApplicationUser_Id");
            CreateIndex("dbo.LoginLogs", "UserId");
            AddForeignKey("dbo.LoginLogs", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
