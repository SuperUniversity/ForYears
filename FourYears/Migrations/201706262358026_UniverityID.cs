namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniverityID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UniversityID", c => c.String());
            DropColumn("dbo.AspNetUsers", "College");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "College", c => c.String());
            DropColumn("dbo.AspNetUsers", "UniversityID");
        }
    }
}
