namespace Almotkaml.ArtificialLimbs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        _Permissions = c.String(),
                    })
                .PrimaryKey(t => t.UserGroupId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        UserGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserGroupId", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "UserGroupId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
        }
    }
}
