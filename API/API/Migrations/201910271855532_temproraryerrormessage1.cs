namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class temproraryerrormessage1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogin", "Validated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogin", "Validated");
        }
    }
}
