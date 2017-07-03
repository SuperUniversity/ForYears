namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userCourseRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Comment",
                c => new
                    {
                        User_CommentId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CourseId = c.String(),
                        CommentId = c.String(),
                        lastModified = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.User_Favorite",
                c => new
                    {
                        User_FavoriteId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CourseId = c.String(),
                        lastModified = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_FavoriteId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.User_Question",
                c => new
                    {
                        User_QuestionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CourseId = c.String(),
                        questionId = c.String(),
                        lastModified = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_QuestionId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.User_Ranking",
                c => new
                    {
                        User_RankingId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CourseId = c.String(),
                        RankingId = c.String(),
                        lastModified = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_RankingId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Ranking", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Question", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Favorite", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Comment", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.User_Ranking", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.User_Question", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.User_Favorite", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.User_Comment", new[] { "ApplicationUser_Id" });
            DropTable("dbo.User_Ranking");
            DropTable("dbo.User_Question");
            DropTable("dbo.User_Favorite");
            DropTable("dbo.User_Comment");
        }
    }
}
