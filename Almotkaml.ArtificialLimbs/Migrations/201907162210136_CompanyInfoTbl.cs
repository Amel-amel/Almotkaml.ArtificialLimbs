namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyInfoTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyInfos",
                c => new
                    {
                        CompanyInfosID = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        LongName = c.String(),
                        EnglishName = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.CompanyInfosID);
            
            CreateTable(
                "dbo.Infoes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Infoes");
            DropTable("dbo.CompanyInfos");
        }
    }
}
