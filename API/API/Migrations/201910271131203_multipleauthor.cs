namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multipleauthor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Book", "Author_AuthorId", "dbo.Author");
            DropIndex("dbo.Book", new[] { "Author_AuthorId" });
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_BookId = c.Int(nullable: false),
                        Author_AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookId, t.Author_AuthorId })
                .ForeignKey("dbo.Book", t => t.Book_BookId, cascadeDelete: true)
                .ForeignKey("dbo.Author", t => t.Author_AuthorId, cascadeDelete: true)
                .Index(t => t.Book_BookId)
                .Index(t => t.Author_AuthorId);
            
            DropColumn("dbo.Book", "Author_AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "Author_AuthorId", c => c.Int());
            DropIndex("dbo.BookAuthors", new[] { "Author_AuthorId" });
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropForeignKey("dbo.BookAuthors", "Author_AuthorId", "dbo.Author");
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Book");
            DropTable("dbo.BookAuthors");
            CreateIndex("dbo.Book", "Author_AuthorId");
            AddForeignKey("dbo.Book", "Author_AuthorId", "dbo.Author", "AuthorId");
        }
    }
}
