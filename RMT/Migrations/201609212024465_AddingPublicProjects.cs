namespace RMT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPublicProjects : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ExternalProject", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projects", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Visible");
            DropColumn("dbo.Projects", "ExternalProject");
        }
    }
}
