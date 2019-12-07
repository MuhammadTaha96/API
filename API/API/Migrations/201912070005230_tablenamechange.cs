namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablenamechange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EletronicFile", newName: "ElectronicFile");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ElectronicFile", newName: "EletronicFile");
        }
    }
}
