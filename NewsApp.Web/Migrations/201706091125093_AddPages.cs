namespace NewsApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddPages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                {
                    PageId = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.PageId);

            Sql("Insert into Pages(title,description) values('About Us', '<p>����� ������(Lorem Ipsum) �� ������ �� ���� (����� �� ������ �� ����� ���� �������) �������� �� ������ ������� ���� �����. ��� ����� ������ ������� ������� ���� ������ ��� ����� ������ ��� ����� ���� ����� ������ ��� ������ �� ������ ���� ������ ������ �� �ա ������ ����� ������ ���� �� ���� ���� ���� ������. ���� ���� �� ����� �� ���� ��� ��� ���ա �� ��� ��� ��� �������� ������ ������ �� ������� �������� ����������. ����� ���� ���� �� �������� ��� ����� �� ����� ����� \"��������\" (Letraset) ����������� ���� ����� �� ��� ���ա ���� ������ ��� ���� ������ �� ���� ����� ����� ���������� ��� \"����� ���� �����\" (Aldus PageMaker) ����� ��� ����� ��� ��� �� �� ����� ������.</p>')");

        }

        public override void Down()
        {
            DropTable("dbo.Pages");
        }
    }
}
