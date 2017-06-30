namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class College : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "College", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "College");
        }
    }
}
