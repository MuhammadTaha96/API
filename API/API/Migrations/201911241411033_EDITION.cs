namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EDITION : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Edition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Edition");
        }
    }
}
