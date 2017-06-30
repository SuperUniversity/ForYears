namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        LogInTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        UniversityId = c.Int(nullable: false, identity: true),
                        EduCode = c.String(maxLength: 256),
                        ChineseName = c.String(maxLength: 256),
                        EnglishName = c.String(maxLength: 256),
                        PostCode = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Phone = c.String(maxLength: 256),
                        Fax = c.String(maxLength: 256),
                        Website = c.String(maxLength: 256),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UniversityId);
            
            AddColumn("dbo.AspNetUsers", "University_UniversityId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "University_UniversityId");
            AddForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities", "UniversityId");
            DropColumn("dbo.AspNetUsers", "UniversityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UniversityId", c => c.Int());
            DropForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities");
            DropForeignKey("dbo.LoginLogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.LoginLogs", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "University_UniversityId" });
            DropColumn("dbo.AspNetUsers", "University_UniversityId");
            DropTable("dbo.Universities");
            DropTable("dbo.LoginLogs");
        }
    }
}
