namespace RMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                        UserName = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        CreatedDate = c.DateTime(),
                        Hidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        PictureName = c.String(),
                        Description = c.String(),
                        Path = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImagePath = c.String(),
                        Status = c.String(),
                        BeginDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Pictures", new[] { "ProjectId" });
            DropIndex("dbo.Comments", new[] { "PictureId" });
            DropTable("dbo.Projects");
            DropTable("dbo.Pictures");
            DropTable("dbo.Comments");
        }
    }
}
