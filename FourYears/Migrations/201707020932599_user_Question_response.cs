namespace FourYears.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_Question_response : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Question_Response",
                c => new
                    {
                        User_Question_ResponseId = c.Int(nullable: false, identity: true),
                        userId = c.String(),
                        courseId = c.String(),
                        questionId = c.String(),
                        reesponseId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.User_Question_ResponseId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Question_Response", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.User_Question_Response", new[] { "ApplicationUser_Id" });
            DropTable("dbo.User_Question_Response");
        }
    }
}
