namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eletronicfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ElectronicFileType",
                c => new
                    {
                        ElectronicFileTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ElectronicFileTypeId);
            
            CreateTable(
                "dbo.EletronicFile",
                c => new
                    {
                        ElectronicFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Description = c.String(),
                        FileType_ElectronicFileTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.ElectronicFileId)
                .ForeignKey("dbo.ElectronicFileType", t => t.FileType_ElectronicFileTypeId)
                .Index(t => t.FileType_ElectronicFileTypeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EletronicFile", new[] { "FileType_ElectronicFileTypeId" });
            DropForeignKey("dbo.EletronicFile", "FileType_ElectronicFileTypeId", "dbo.ElectronicFileType");
            DropTable("dbo.EletronicFile");
            DropTable("dbo.ElectronicFileType");
        }
    }
}
