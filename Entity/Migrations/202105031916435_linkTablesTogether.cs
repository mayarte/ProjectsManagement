namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkTablesTogether : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clearances", "SystemUserGroup_ID", "dbo.SystemUserGroups");
            DropIndex("dbo.Clearances", new[] { "SystemUserGroup_ID" });
            CreateTable(
                "dbo.SystemUserGroupClearances",
                c => new
                    {
                        SystemUserGroup_ID = c.Int(nullable: false),
                        Clearance_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SystemUserGroup_ID, t.Clearance_ID })
                .ForeignKey("dbo.SystemUserGroups", t => t.SystemUserGroup_ID, cascadeDelete: true)
                .ForeignKey("dbo.Clearances", t => t.Clearance_ID, cascadeDelete: true)
                .Index(t => t.SystemUserGroup_ID)
                .Index(t => t.Clearance_ID);
            
            DropColumn("dbo.Clearances", "SystemUserGroup_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clearances", "SystemUserGroup_ID", c => c.Int());
            DropForeignKey("dbo.SystemUserGroupClearances", "Clearance_ID", "dbo.Clearances");
            DropForeignKey("dbo.SystemUserGroupClearances", "SystemUserGroup_ID", "dbo.SystemUserGroups");
            DropIndex("dbo.SystemUserGroupClearances", new[] { "Clearance_ID" });
            DropIndex("dbo.SystemUserGroupClearances", new[] { "SystemUserGroup_ID" });
            DropTable("dbo.SystemUserGroupClearances");
            CreateIndex("dbo.Clearances", "SystemUserGroup_ID");
            AddForeignKey("dbo.Clearances", "SystemUserGroup_ID", "dbo.SystemUserGroups", "ID");
        }
    }
}
