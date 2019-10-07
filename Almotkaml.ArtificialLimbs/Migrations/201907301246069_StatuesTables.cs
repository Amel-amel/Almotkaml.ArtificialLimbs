namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatuesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmputationDetails",
                c => new
                    {
                        AmputationDetailsID = c.Int(nullable: false, identity: true),
                        AmputationStatuesID = c.Int(nullable: false),
                        Part = c.Int(),
                        PartMeasure = c.Int(),
                        ShoeNO = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AmputationDetailsID)
                .ForeignKey("dbo.AmputationStatues", t => t.AmputationStatuesID, cascadeDelete: true)
                .Index(t => t.AmputationStatuesID);
            
            CreateTable(
                "dbo.AmputationStatues",
                c => new
                    {
                        AmputationStatuesID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        TechnicalID = c.Int(),
                        TechnicalAssistantID = c.Int(),
                        DeviceID = c.Int(nullable: false),
                        MeasurementDate = c.String(),
                        FirstTestDate = c.String(),
                        SecondTestDate = c.String(),
                        ReceiptDate = c.String(),
                        ReferenceNo = c.Int(),
                        AmputationTypeID = c.Int(nullable: false),
                        AmputationPart = c.Int(nullable: false),
                        AmputationReason = c.String(),
                        AmputationDate = c.String(),
                        Note = c.String(),
                        Technical_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.AmputationStatuesID)
                .ForeignKey("dbo.AmputationTypes", t => t.AmputationTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.TechnicalAssistantID)
                .ForeignKey("dbo.Devices", t => t.DeviceID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Technical_EmployeeID)
                .Index(t => t.PatientID)
                .Index(t => t.TechnicalAssistantID)
                .Index(t => t.DeviceID)
                .Index(t => t.AmputationTypeID)
                .Index(t => t.Technical_EmployeeID);
            
            CreateTable(
                "dbo.DailyStatues",
                c => new
                    {
                        DailyStatuesID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        TechnicalID = c.Int(),
                        TechnicalAssistantID = c.Int(),
                        DeviceID = c.Int(nullable: false),
                        MeasurementDate = c.String(),
                        ReceiptDate = c.String(),
                        ReferenceNo = c.Int(),
                        Note = c.String(),
                        Technical_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.DailyStatuesID)
                .ForeignKey("dbo.Devices", t => t.DeviceID, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Technical_EmployeeID)
                .ForeignKey("dbo.Employees", t => t.TechnicalAssistantID)
                .Index(t => t.PatientID)
                .Index(t => t.TechnicalAssistantID)
                .Index(t => t.DeviceID)
                .Index(t => t.Technical_EmployeeID);
            
            CreateTable(
                "dbo.DailyDetails",
                c => new
                    {
                        DailyDetailsID = c.Int(nullable: false, identity: true),
                        DailyStatuesID = c.Int(nullable: false),
                        Part = c.Int(),
                        PartMeasure = c.Int(),
                        ShoeNO = c.Int(),
                    })
                .PrimaryKey(t => t.DailyDetailsID)
                .ForeignKey("dbo.DailyStatues", t => t.DailyStatuesID, cascadeDelete: true)
                .Index(t => t.DailyStatuesID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AmputationStatues", "Technical_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.AmputationStatues", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.AmputationStatues", "DeviceID", "dbo.Devices");
            DropForeignKey("dbo.DailyStatues", "TechnicalAssistantID", "dbo.Employees");
            DropForeignKey("dbo.DailyStatues", "Technical_EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.DailyStatues", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.DailyStatues", "DeviceID", "dbo.Devices");
            DropForeignKey("dbo.DailyDetails", "DailyStatuesID", "dbo.DailyStatues");
            DropForeignKey("dbo.AmputationStatues", "TechnicalAssistantID", "dbo.Employees");
            DropForeignKey("dbo.AmputationStatues", "AmputationTypeID", "dbo.AmputationTypes");
            DropForeignKey("dbo.AmputationDetails", "AmputationStatuesID", "dbo.AmputationStatues");
            DropIndex("dbo.DailyDetails", new[] { "DailyStatuesID" });
            DropIndex("dbo.DailyStatues", new[] { "Technical_EmployeeID" });
            DropIndex("dbo.DailyStatues", new[] { "DeviceID" });
            DropIndex("dbo.DailyStatues", new[] { "TechnicalAssistantID" });
            DropIndex("dbo.DailyStatues", new[] { "PatientID" });
            DropIndex("dbo.AmputationStatues", new[] { "Technical_EmployeeID" });
            DropIndex("dbo.AmputationStatues", new[] { "AmputationTypeID" });
            DropIndex("dbo.AmputationStatues", new[] { "DeviceID" });
            DropIndex("dbo.AmputationStatues", new[] { "TechnicalAssistantID" });
            DropIndex("dbo.AmputationStatues", new[] { "PatientID" });
            DropIndex("dbo.AmputationDetails", new[] { "AmputationStatuesID" });
            DropTable("dbo.DailyDetails");
            DropTable("dbo.DailyStatues");
            DropTable("dbo.AmputationStatues");
            DropTable("dbo.AmputationDetails");
        }
    }
}
