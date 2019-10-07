namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientReports",
                c => new
                    {
                        PatientReportID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        Description = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.PatientReportID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                        NationalityNumber = c.String(),
                        NationalityID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Nationalities", t => t.NationalityID, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.NationalityID)
                .Index(t => t.CityID)
                .Index(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Patients", "NationalityID", "dbo.Nationalities");
            DropForeignKey("dbo.Patients", "CityID", "dbo.Cities");
            DropForeignKey("dbo.PatientReports", "PatientID", "dbo.Patients");
            DropIndex("dbo.Patients", new[] { "PlaceId" });
            DropIndex("dbo.Patients", new[] { "CityID" });
            DropIndex("dbo.Patients", new[] { "NationalityID" });
            DropIndex("dbo.PatientReports", new[] { "PatientID" });
            DropTable("dbo.Patients");
            DropTable("dbo.PatientReports");
        }
    }
}
