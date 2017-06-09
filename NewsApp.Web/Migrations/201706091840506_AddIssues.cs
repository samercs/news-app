namespace NewsApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIssues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Issues");
        }
    }
}
