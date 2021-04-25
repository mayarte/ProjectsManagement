namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectRelatedStuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(),
                        LastUpdateTime = c.DateTime(),
                        Text = c.String(),
                        MoreInfo = c.String(),
                        PostType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        PostedBy_ID = c.Int(),
                        Project_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUsers", t => t.PostedBy_ID)
                .ForeignKey("dbo.ProjectDatas", t => t.Project_ID)
                .Index(t => t.PostedBy_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.ProjectDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreationDate = c.DateTime(),
                        ProgressPercentage = c.Int(nullable: false),
                        ProjectStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ProposedBy_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SystemUsers", t => t.ProposedBy_ID)
                .Index(t => t.ProposedBy_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectPosts", "Project_ID", "dbo.ProjectDatas");
            DropForeignKey("dbo.ProjectDatas", "ProposedBy_ID", "dbo.SystemUsers");
            DropForeignKey("dbo.ProjectPosts", "PostedBy_ID", "dbo.SystemUsers");
            DropIndex("dbo.ProjectDatas", new[] { "ProposedBy_ID" });
            DropIndex("dbo.ProjectPosts", new[] { "Project_ID" });
            DropIndex("dbo.ProjectPosts", new[] { "PostedBy_ID" });
            DropTable("dbo.ProjectDatas");
            DropTable("dbo.ProjectPosts");
        }
    }
}
