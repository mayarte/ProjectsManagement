namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clearances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemUserGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        ClearanceGroup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUserGroups", t => t.ClearanceGroup_ID)
                .Index(t => t.ClearanceGroup_ID);
            
            CreateTable(
                "dbo.ClearanceSystemUserGroups",
                c => new
                    {
                        Clearance_ID = c.Int(nullable: false),
                        SystemUserGroup_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clearance_ID, t.SystemUserGroup_ID })
                .ForeignKey("dbo.Clearances", t => t.Clearance_ID, cascadeDelete: true)
                .ForeignKey("dbo.SystemUserGroups", t => t.SystemUserGroup_ID, cascadeDelete: true)
                .Index(t => t.Clearance_ID)
                .Index(t => t.SystemUserGroup_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemUsers", "ClearanceGroup_ID", "dbo.SystemUserGroups");
            DropForeignKey("dbo.ClearanceSystemUserGroups", "SystemUserGroup_ID", "dbo.SystemUserGroups");
            DropForeignKey("dbo.ClearanceSystemUserGroups", "Clearance_ID", "dbo.Clearances");
            DropIndex("dbo.ClearanceSystemUserGroups", new[] { "SystemUserGroup_ID" });
            DropIndex("dbo.ClearanceSystemUserGroups", new[] { "Clearance_ID" });
            DropIndex("dbo.SystemUsers", new[] { "ClearanceGroup_ID" });
            DropTable("dbo.ClearanceSystemUserGroups");
            DropTable("dbo.SystemUsers");
            DropTable("dbo.SystemUserGroups");
            DropTable("dbo.Clearances");
        }
    }
}
