namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationStatus",
                c => new
                    {
                        ReservationStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ReservationStatusId);
            
            CreateTable(
                "dbo.TransactionType",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        ExpectedReturnDate = c.DateTime(nullable: false),
                        Fine = c.Int(nullable: false),
                        DaysKept = c.Int(nullable: false),
                        Reservation_ReservationId = c.Int(),
                        User_UserLoginId = c.Int(),
                        Copy_CopyId = c.Int(),
                        Type_TransactionTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Reservation", t => t.Reservation_ReservationId)
                .ForeignKey("dbo.UserLogin", t => t.User_UserLoginId)
                .ForeignKey("dbo.Copy", t => t.Copy_CopyId)
                .ForeignKey("dbo.TransactionType", t => t.Type_TransactionTypeId)
                .Index(t => t.Reservation_ReservationId)
                .Index(t => t.User_UserLoginId)
                .Index(t => t.Copy_CopyId)
                .Index(t => t.Type_TransactionTypeId);
            
            AddColumn("dbo.Reservation", "Status_ReservationStatusId", c => c.Int());
            AddForeignKey("dbo.Reservation", "Status_ReservationStatusId", "dbo.ReservationStatus", "ReservationStatusId");
            CreateIndex("dbo.Reservation", "Status_ReservationStatusId");
            DropColumn("dbo.Reservation", "IsCancled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "IsCancled", c => c.Boolean(nullable: false));
            DropIndex("dbo.Transaction", new[] { "Type_TransactionTypeId" });
            DropIndex("dbo.Transaction", new[] { "Copy_CopyId" });
            DropIndex("dbo.Transaction", new[] { "User_UserLoginId" });
            DropIndex("dbo.Transaction", new[] { "Reservation_ReservationId" });
            DropIndex("dbo.Reservation", new[] { "Status_ReservationStatusId" });
            DropForeignKey("dbo.Transaction", "Type_TransactionTypeId", "dbo.TransactionType");
            DropForeignKey("dbo.Transaction", "Copy_CopyId", "dbo.Copy");
            DropForeignKey("dbo.Transaction", "User_UserLoginId", "dbo.UserLogin");
            DropForeignKey("dbo.Transaction", "Reservation_ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Reservation", "Status_ReservationStatusId", "dbo.ReservationStatus");
            DropColumn("dbo.Reservation", "Status_ReservationStatusId");
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionType");
            DropTable("dbo.ReservationStatus");
        }
    }
}
