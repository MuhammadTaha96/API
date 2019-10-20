namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isbn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "ISBN_No", c => c.String());
            DropColumn("dbo.Copy", "ISBN_No");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Copy", "ISBN_No", c => c.String());
            DropColumn("dbo.Book", "ISBN_No");
        }
    }
}
