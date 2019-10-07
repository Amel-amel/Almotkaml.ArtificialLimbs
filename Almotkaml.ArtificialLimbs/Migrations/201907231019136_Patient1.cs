namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patient1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientName", c => c.String());
            DropColumn("dbo.Patients", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "Name", c => c.String());
            DropColumn("dbo.Patients", "PatientName");
        }
    }
}
