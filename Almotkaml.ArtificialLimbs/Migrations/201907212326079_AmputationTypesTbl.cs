namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmputationTypesTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmputationTypes",
                c => new
                    {
                        AmputationTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AmputationTypeID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AmputationTypes");
        }
    }
}
