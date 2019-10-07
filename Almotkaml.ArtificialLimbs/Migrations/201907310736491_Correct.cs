namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AmputationStatues", "TechnicalID");
            DropColumn("dbo.DailyStatues", "TechnicalID");
            RenameColumn(table: "dbo.AmputationStatues", name: "Technical_EmployeeID", newName: "TechnicalID");
            RenameColumn(table: "dbo.DailyStatues", name: "Technical_EmployeeID", newName: "TechnicalID");
            RenameIndex(table: "dbo.AmputationStatues", name: "IX_Technical_EmployeeID", newName: "IX_TechnicalID");
            RenameIndex(table: "dbo.DailyStatues", name: "IX_Technical_EmployeeID", newName: "IX_TechnicalID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DailyStatues", name: "IX_TechnicalID", newName: "IX_Technical_EmployeeID");
            RenameIndex(table: "dbo.AmputationStatues", name: "IX_TechnicalID", newName: "IX_Technical_EmployeeID");
            RenameColumn(table: "dbo.DailyStatues", name: "TechnicalID", newName: "Technical_EmployeeID");
            RenameColumn(table: "dbo.AmputationStatues", name: "TechnicalID", newName: "Technical_EmployeeID");
            AddColumn("dbo.DailyStatues", "TechnicalID", c => c.Int());
            AddColumn("dbo.AmputationStatues", "TechnicalID", c => c.Int());
        }
    }
}
