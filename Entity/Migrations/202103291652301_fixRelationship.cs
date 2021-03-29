namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClearanceSystemUserGroups", "Clearance_ID", "dbo.Clearances");
            DropForeignKey("dbo.ClearanceSystemUserGroups", "SystemUserGroup_ID", "dbo.SystemUserGroups");
            DropIndex("dbo.ClearanceSystemUserGroups", new[] { "Clearance_ID" });
            DropIndex("dbo.ClearanceSystemUserGroups", new[] { "SystemUserGroup_ID" });
            AddColumn("dbo.Clearances", "SystemUserGroup_ID", c => c.Int());
            CreateIndex("dbo.Clearances", "SystemUserGroup_ID");
            AddForeignKey("dbo.Clearances", "SystemUserGroup_ID", "dbo.SystemUserGroups", "ID");
            DropTable("dbo.ClearanceSystemUserGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClearanceSystemUserGroups",
                c => new
                    {
                        Clearance_ID = c.Int(nullable: false),
                        SystemUserGroup_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Clearance_ID, t.SystemUserGroup_ID });
            
            DropForeignKey("dbo.Clearances", "SystemUserGroup_ID", "dbo.SystemUserGroups");
            DropIndex("dbo.Clearances", new[] { "SystemUserGroup_ID" });
            DropColumn("dbo.Clearances", "SystemUserGroup_ID");
            CreateIndex("dbo.ClearanceSystemUserGroups", "SystemUserGroup_ID");
            CreateIndex("dbo.ClearanceSystemUserGroups", "Clearance_ID");
            AddForeignKey("dbo.ClearanceSystemUserGroups", "SystemUserGroup_ID", "dbo.SystemUserGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ClearanceSystemUserGroups", "Clearance_ID", "dbo.Clearances", "ID", cascadeDelete: true);
        }
    }
}
