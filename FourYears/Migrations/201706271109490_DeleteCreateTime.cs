namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCreateTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "CreateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CreateTime", c => c.DateTime(nullable: false));
        }
    }
}
