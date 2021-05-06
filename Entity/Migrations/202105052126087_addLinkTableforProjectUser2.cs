namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLinkTableforProjectUser2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectUserLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LinkType = c.Int(nullable: false),
                        Project_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectDatas", t => t.Project_ID)
                .ForeignKey("dbo.SystemUsers", t => t.User_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUserLinks", "User_ID", "dbo.SystemUsers");
            DropForeignKey("dbo.ProjectUserLinks", "Project_ID", "dbo.ProjectDatas");
            DropIndex("dbo.ProjectUserLinks", new[] { "User_ID" });
            DropIndex("dbo.ProjectUserLinks", new[] { "Project_ID" });
            DropTable("dbo.ProjectUserLinks");
        }
    }
}
