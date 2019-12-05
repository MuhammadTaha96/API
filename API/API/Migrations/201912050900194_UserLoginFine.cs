namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLoginFine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLoginFine",
                c => new
                    {
                        UserLoginFineId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Defaulter_UserLoginId = c.Int(),
                    })
                .PrimaryKey(t => t.UserLoginFineId)
                .ForeignKey("dbo.UserLogin", t => t.Defaulter_UserLoginId)
                .Index(t => t.Defaulter_UserLoginId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserLoginFine", new[] { "Defaulter_UserLoginId" });
            DropForeignKey("dbo.UserLoginFine", "Defaulter_UserLoginId", "dbo.UserLogin");
            DropTable("dbo.UserLoginFine");
        }
    }
}
