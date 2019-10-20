namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "ImagePath");
        }
    }
}
