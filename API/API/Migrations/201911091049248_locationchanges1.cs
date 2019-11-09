namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationchanges1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "ShelfCol", c => c.Int(nullable: false));
            DropColumn("dbo.Location", "ShelfColumn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Location", "ShelfColumn", c => c.Int(nullable: false));
            DropColumn("dbo.Location", "ShelfCol");
        }
    }
}
