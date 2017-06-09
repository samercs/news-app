namespace NewsApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFavorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFavorites",
                c => new
                    {
                        UserFavoriteId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserFavoriteId)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavorites", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFavorites", "NewsId", "dbo.News");
            DropIndex("dbo.UserFavorites", new[] { "NewsId" });
            DropIndex("dbo.UserFavorites", new[] { "UserId" });
            DropTable("dbo.UserFavorites");
        }
    }
}
