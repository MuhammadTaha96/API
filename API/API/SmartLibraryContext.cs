using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Models;
using Models.Models;

namespace API
{
    public class SmartLibraryContext : DbContext
    {
        public SmartLibraryContext()
            : base("name=SmartLibraryConnection")
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Ebook> Ebook { get; set; }
        public DbSet<ReservationStatus> ReservationStatus { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserLoginFine> UserLoginFines { get; set; }
        public DbSet<ElectronicFileType> ElectronicFileTypes { get; set; }
        public DbSet<ElectronicFile> ElectronicFiles { get; set; } 
    }
}