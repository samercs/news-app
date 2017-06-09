namespace NewsApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddAspNetAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("insert into dbo.AspNetRoles (Id, Name) values ('" + Guid.NewGuid().ToString().ToLower() + "', 'Administrator');");
        }

        public override void Down()
        {
            Sql("delete from dbo.AspNetUserRoles");
            Sql("delete from dbo.AspNetRoles");
        }
    }
}
