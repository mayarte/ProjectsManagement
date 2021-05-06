namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLinkTableforProjectUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectDatas", "ProposedBy_ID", "dbo.SystemUsers");
            DropIndex("dbo.ProjectDatas", new[] { "ProposedBy_ID" });
            DropColumn("dbo.ProjectDatas", "ProposedBy_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectDatas", "ProposedBy_ID", c => c.Int());
            CreateIndex("dbo.ProjectDatas", "ProposedBy_ID");
            AddForeignKey("dbo.ProjectDatas", "ProposedBy_ID", "dbo.SystemUsers", "ID");
        }
    }
}
