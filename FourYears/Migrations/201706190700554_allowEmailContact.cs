namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowEmailContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "allowEmailContact", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "allowEmailContact");
        }
    }
}
