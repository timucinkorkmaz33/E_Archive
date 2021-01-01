namespace SmartAdminMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Project_General", "CompanyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project_General", "CompanyName", c => c.String());
        }
    }
}
