namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "UserId", "dbo.Users");
            DropIndex("dbo.Activities", new[] { "UserId" });
            DropTable("dbo.Activities");
        }
    }
}
