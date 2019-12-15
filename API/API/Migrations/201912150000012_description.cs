namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ElectronicFileType", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ElectronicFileType", "Description");
        }
    }
}
