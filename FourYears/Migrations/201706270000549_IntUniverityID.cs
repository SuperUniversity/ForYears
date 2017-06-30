namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntUniverityID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UniversityID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UniversityID", c => c.String());
        }
    }
}
