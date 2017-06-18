namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ActualName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ActualName");
        }
    }
}
