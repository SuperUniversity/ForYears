namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLastmodifiedProp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User_Comment", "lastModified");
            DropColumn("dbo.User_Favorite", "lastModified");
            DropColumn("dbo.User_Question", "lastModified");
            DropColumn("dbo.User_Ranking", "lastModified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Ranking", "lastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.User_Question", "lastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.User_Favorite", "lastModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.User_Comment", "lastModified", c => c.DateTime(nullable: false));
        }
    }
}
