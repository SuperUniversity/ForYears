namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniversityId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities");
            DropIndex("dbo.AspNetUsers", new[] { "University_UniversityId" });
            AddColumn("dbo.AspNetUsers", "UniversityId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "University_UniversityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "University_UniversityId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "UniversityId");
            CreateIndex("dbo.AspNetUsers", "University_UniversityId");
            AddForeignKey("dbo.AspNetUsers", "University_UniversityId", "dbo.Universities", "UniversityId");
        }
    }
}
