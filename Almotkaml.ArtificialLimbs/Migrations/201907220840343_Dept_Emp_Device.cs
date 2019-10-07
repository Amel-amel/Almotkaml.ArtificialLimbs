namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dept_Emp_Device : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceID = c.Int(nullable: false, identity: true),
                        ArabicName = c.String(),
                        EnglishName = c.String(),
                        DepartmentID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Devices", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentID" });
            DropIndex("dbo.Devices", new[] { "DepartmentID" });
            DropTable("dbo.Employees");
            DropTable("dbo.Devices");
            DropTable("dbo.Departments");
        }
    }
}
