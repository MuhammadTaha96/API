namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class temproraryerrormessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogin", "ValidationErrorMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogin", "ValidationErrorMessage");
        }
    }
}
