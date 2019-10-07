namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailsTbls11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyDetails", "Note", c => c.String());
            DropColumn("dbo.DailyDetails", "AmputationPart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyDetails", "AmputationPart", c => c.Int(nullable: false));
            DropColumn("dbo.DailyDetails", "Note");
        }
    }
}
