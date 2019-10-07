namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityCountryBranchPlaceHospitalNationaltyTbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BranchId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        HospitalsId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.HospitalsId);
            
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        NationalityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.NationalityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Places", "BranchId", "dbo.Branches");
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Places", new[] { "BranchId" });
            DropTable("dbo.Nationalities");
            DropTable("dbo.Hospitals");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Places");
            DropTable("dbo.Branches");
        }
    }
}
