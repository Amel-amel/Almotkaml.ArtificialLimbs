namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailsTbls1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AmputationDetails", "AmputationPart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AmputationDetails", "AmputationPart", c => c.Int(nullable: false));
        }
    }
}
