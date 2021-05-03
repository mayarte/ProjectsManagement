namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yearsAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyYears",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Year1 = c.String(),
                        Year2 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProjectDatas", "Year_ID", c => c.Int());
            CreateIndex("dbo.ProjectDatas", "Year_ID");
            AddForeignKey("dbo.ProjectDatas", "Year_ID", "dbo.StudyYears", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectDatas", "Year_ID", "dbo.StudyYears");
            DropIndex("dbo.ProjectDatas", new[] { "Year_ID" });
            DropColumn("dbo.ProjectDatas", "Year_ID");
            DropTable("dbo.StudyYears");
        }
    }
}
