namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isDeletedbit1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publisher", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publisher", "Address");
        }
    }
}
