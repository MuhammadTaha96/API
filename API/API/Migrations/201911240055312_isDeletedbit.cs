namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isDeletedbit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogin", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogin", "IsDeleted");
        }
    }
}
