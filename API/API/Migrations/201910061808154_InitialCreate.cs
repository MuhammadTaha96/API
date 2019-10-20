namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Language = c.String(),
                        PublishedYear = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Author_AuthorId = c.Int(),
                        Publisher_PublisherId = c.Int(),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Author", t => t.Author_AuthorId)
                .ForeignKey("dbo.Publisher", t => t.Publisher_PublisherId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .Index(t => t.Author_AuthorId)
                .Index(t => t.Publisher_PublisherId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Reviewer = c.String(),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Book", t => t.Book_BookId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Rating = c.Int(nullable: false),
                        Commenter_UserLoginId = c.Int(),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.UserLogin", t => t.Commenter_UserLoginId)
                .ForeignKey("dbo.Book", t => t.Book_BookId)
                .Index(t => t.Commenter_UserLoginId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserLoginId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        RFID = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        UserType_UserTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.UserLoginId)
                .ForeignKey("dbo.UserType", t => t.UserType_UserTypeId)
                .Index(t => t.UserType_UserTypeId);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
            CreateTable(
                "dbo.Copy",
                c => new
                    {
                        CopyId = c.Int(nullable: false, identity: true),
                        ISBN_No = c.String(),
                        RFID = c.String(),
                        Status_StatusId = c.Int(),
                        Book_BookId = c.Int(),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.CopyId)
                .ForeignKey("dbo.Status", t => t.Status_StatusId)
                .ForeignKey("dbo.Book", t => t.Book_BookId)
                .ForeignKey("dbo.Location", t => t.Location_LocationId)
                .Index(t => t.Status_StatusId)
                .Index(t => t.Book_BookId)
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Shelf = c.Int(nullable: false),
                        ShelfLine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        IsCancled = c.Boolean(nullable: false),
                        ReservedCopy_CopyId = c.Int(),
                        ReservedBy_UserLoginId = c.Int(),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Copy", t => t.ReservedCopy_CopyId)
                .ForeignKey("dbo.UserLogin", t => t.ReservedBy_UserLoginId)
                .ForeignKey("dbo.Book", t => t.Book_BookId)
                .Index(t => t.ReservedCopy_CopyId)
                .Index(t => t.ReservedBy_UserLoginId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.IssueReport",
                c => new
                    {
                        IssueReportId = c.Int(nullable: false, identity: true),
                        IssueDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Fine = c.Int(nullable: false),
                        Returned = c.Boolean(nullable: false),
                        Copy_CopyId = c.Int(),
                    })
                .PrimaryKey(t => t.IssueReportId)
                .ForeignKey("dbo.Copy", t => t.Copy_CopyId)
                .Index(t => t.Copy_CopyId);
            
            CreateTable(
                "dbo.EBook",
                c => new
                    {
                        EbookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author_AuthorId = c.Int(),
                    })
                .PrimaryKey(t => t.EbookId)
                .ForeignKey("dbo.Author", t => t.Author_AuthorId)
                .Index(t => t.Author_AuthorId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EBook", new[] { "Author_AuthorId" });
            DropIndex("dbo.IssueReport", new[] { "Copy_CopyId" });
            DropIndex("dbo.Reservation", new[] { "Book_BookId" });
            DropIndex("dbo.Reservation", new[] { "ReservedBy_UserLoginId" });
            DropIndex("dbo.Reservation", new[] { "ReservedCopy_CopyId" });
            DropIndex("dbo.Copy", new[] { "Location_LocationId" });
            DropIndex("dbo.Copy", new[] { "Book_BookId" });
            DropIndex("dbo.Copy", new[] { "Status_StatusId" });
            DropIndex("dbo.UserLogin", new[] { "UserType_UserTypeId" });
            DropIndex("dbo.Comment", new[] { "Book_BookId" });
            DropIndex("dbo.Comment", new[] { "Commenter_UserLoginId" });
            DropIndex("dbo.Review", new[] { "Book_BookId" });
            DropIndex("dbo.Book", new[] { "Category_CategoryId" });
            DropIndex("dbo.Book", new[] { "Publisher_PublisherId" });
            DropIndex("dbo.Book", new[] { "Author_AuthorId" });
            DropForeignKey("dbo.EBook", "Author_AuthorId", "dbo.Author");
            DropForeignKey("dbo.IssueReport", "Copy_CopyId", "dbo.Copy");
            DropForeignKey("dbo.Reservation", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.Reservation", "ReservedBy_UserLoginId", "dbo.UserLogin");
            DropForeignKey("dbo.Reservation", "ReservedCopy_CopyId", "dbo.Copy");
            DropForeignKey("dbo.Copy", "Location_LocationId", "dbo.Location");
            DropForeignKey("dbo.Copy", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.Copy", "Status_StatusId", "dbo.Status");
            DropForeignKey("dbo.UserLogin", "UserType_UserTypeId", "dbo.UserType");
            DropForeignKey("dbo.Comment", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.Comment", "Commenter_UserLoginId", "dbo.UserLogin");
            DropForeignKey("dbo.Review", "Book_BookId", "dbo.Book");
            DropForeignKey("dbo.Book", "Category_CategoryId", "dbo.Category");
            DropForeignKey("dbo.Book", "Publisher_PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "Author_AuthorId", "dbo.Author");
            DropTable("dbo.EBook");
            DropTable("dbo.IssueReport");
            DropTable("dbo.Reservation");
            DropTable("dbo.Location");
            DropTable("dbo.Status");
            DropTable("dbo.Copy");
            DropTable("dbo.UserType");
            DropTable("dbo.UserLogin");
            DropTable("dbo.Comment");
            DropTable("dbo.Review");
            DropTable("dbo.Category");
            DropTable("dbo.Publisher");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
