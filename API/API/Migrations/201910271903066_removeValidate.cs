namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeValidate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserLogin", "Validated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserLogin", "Validated", c => c.Boolean(nullable: false));
        }
    }
}
