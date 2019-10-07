namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "PlaceId", "dbo.Places");
            DropIndex("dbo.Patients", new[] { "PlaceId" });
            AlterColumn("dbo.Patients", "PlaceId", c => c.Int());
            CreateIndex("dbo.Patients", "PlaceId");
            AddForeignKey("dbo.Patients", "PlaceId", "dbo.Places", "PlaceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "PlaceId", "dbo.Places");
            DropIndex("dbo.Patients", new[] { "PlaceId" });
            AlterColumn("dbo.Patients", "PlaceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "PlaceId");
            AddForeignKey("dbo.Patients", "PlaceId", "dbo.Places", "PlaceId", cascadeDelete: true);
        }
    }
}
