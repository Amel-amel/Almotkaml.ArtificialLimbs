namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailsTbls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AmputationDetails", "MeasurementModel", c => c.Int(nullable: false));
            AddColumn("dbo.AmputationDetails", "AmputationPart", c => c.Int(nullable: false));
            AddColumn("dbo.AmputationDetails", "shoesNo", c => c.Int());
            AddColumn("dbo.AmputationDetails", "Note", c => c.String());
            AddColumn("dbo.DailyDetails", "MeasurementModel", c => c.Int(nullable: false));
            AddColumn("dbo.DailyDetails", "AmputationPart", c => c.Int(nullable: false));
            AddColumn("dbo.DailyDetails", "shoesNo", c => c.Int());
            AlterColumn("dbo.AmputationDetails", "Part", c => c.Int(nullable: false));
            AlterColumn("dbo.AmputationDetails", "PartMeasure", c => c.Double());
            AlterColumn("dbo.DailyDetails", "Part", c => c.Int(nullable: false));
            AlterColumn("dbo.DailyDetails", "PartMeasure", c => c.Double());
            DropColumn("dbo.AmputationDetails", "ShoeNO");
            DropColumn("dbo.DailyDetails", "ShoeNO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyDetails", "ShoeNO", c => c.Int());
            AddColumn("dbo.AmputationDetails", "ShoeNO", c => c.Int(nullable: false));
            AlterColumn("dbo.DailyDetails", "PartMeasure", c => c.Int());
            AlterColumn("dbo.DailyDetails", "Part", c => c.Int());
            AlterColumn("dbo.AmputationDetails", "PartMeasure", c => c.Int());
            AlterColumn("dbo.AmputationDetails", "Part", c => c.Int());
            DropColumn("dbo.DailyDetails", "shoesNo");
            DropColumn("dbo.DailyDetails", "AmputationPart");
            DropColumn("dbo.DailyDetails", "MeasurementModel");
            DropColumn("dbo.AmputationDetails", "Note");
            DropColumn("dbo.AmputationDetails", "shoesNo");
            DropColumn("dbo.AmputationDetails", "AmputationPart");
            DropColumn("dbo.AmputationDetails", "MeasurementModel");
        }
    }
}
