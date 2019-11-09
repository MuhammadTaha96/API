namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class locationchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "ShelfRow", c => c.Int(nullable: false));
            AddColumn("dbo.Location", "ShelfColumn", c => c.Int(nullable: false));
            DropColumn("dbo.Location", "ShelfLine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Location", "ShelfLine", c => c.Int(nullable: false));
            DropColumn("dbo.Location", "ShelfColumn");
            DropColumn("dbo.Location", "ShelfRow");
        }
    }
}
