namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project_General", "Owner", c => c.String(nullable: false));
            AddColumn("dbo.Project_General", "CompanyName", c => c.String());
            DropColumn("dbo.Project_General", "UserName");
            DropColumn("dbo.Project_General", "Pass");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project_General", "Pass", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Project_General", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Project_General", "CompanyName");
            DropColumn("dbo.Project_General", "Owner");
        }
    }
}
