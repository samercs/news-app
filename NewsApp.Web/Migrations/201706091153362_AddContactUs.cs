namespace NewsApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddContactUs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                {
                    ContactUsId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Title = c.String(),
                    Message = c.String(),
                    IsRead = c.Boolean(nullable: false, defaultValue: false),
                    AddedDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                })
                .PrimaryKey(t => t.ContactUsId);

        }

        public override void Down()
        {
            DropTable("dbo.ContactUs");
        }
    }
}
