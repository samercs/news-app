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

            Sql("Insert into Pages(title,description) values('About Us', '<p>бж—нг ≈н»”жг(Lorem Ipsum) еж »»”«Ў… д’ ‘ябн (»гЏдм √д «бџ«н… ен «б‘яб жбн” «бгЌ жм) жнх” ќѕг Ён ’д«Џ«  «бгЎ«»Џ жѕж— «бд‘—. я«д бж—нг ≈н»”жг жб«н“«б «бгЏн«— ббд’ «б‘ябн гд– «бё—д «бќ«г” Џ‘— Џдѕг« ё«г  гЎ»Џ… гћежб… »—’ гћгжЏ… гд «б√Ќ—Ё »‘яб Џ‘ж«∆н √ќ– е« гд д’° б яжшд я нш» »гЋ«»… ѕбнб √ж г—ћЏ ‘ябн бе–е «б√Ќ—Ё. ќг”… ё—жд гд «б“гд бг  ё÷н Џбм е–« «бд’° »б «де Ќ м ’«— г” ќѕг«р ж»‘ябе «б√’бн Ён «бЎ»«Џ… ж«б д÷нѕ «б≈бя —ждн. «д ‘— »‘яб я»н— Ён ” нднш«  е–« «бё—д гЏ ≈’ѕ«— —ё«∆ё \"бн —«”н \" (Letraset) «б»б«” нян…  Ќжн гё«ЎЏ гд е–« «бд’° жЏ«ѕ бнд ‘— г—… √ќ—м гƒќ—«у гЏ ўеж— »—«гћ «бд‘— «б≈бя —ждн гЋб \"√бѕж” »«нћ г«ня—\" (Aldus PageMaker) ж«б н Ќж  √н÷«р Џбм д”ќ гд д’ бж—нг ≈н»”жг.</p>')");

        }

        public override void Down()
        {
            DropTable("dbo.Pages");
        }
    }
}
