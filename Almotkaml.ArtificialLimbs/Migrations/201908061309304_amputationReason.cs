namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amputationReason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmputationReasons",
                c => new
                    {
                        AmputationReasonID = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                    })
                .PrimaryKey(t => t.AmputationReasonID);
            
            AddColumn("dbo.AmputationStatues", "AmputationReasonID", c => c.Int(nullable: false));
            CreateIndex("dbo.AmputationStatues", "AmputationReasonID");
            AddForeignKey("dbo.AmputationStatues", "AmputationReasonID", "dbo.AmputationReasons", "AmputationReasonID", cascadeDelete: true);
            DropColumn("dbo.AmputationStatues", "AmputationReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AmputationStatues", "AmputationReason", c => c.String());
            DropForeignKey("dbo.AmputationStatues", "AmputationReasonID", "dbo.AmputationReasons");
            DropIndex("dbo.AmputationStatues", new[] { "AmputationReasonID" });
            DropColumn("dbo.AmputationStatues", "AmputationReasonID");
            DropTable("dbo.AmputationReasons");
        }
    }
}
