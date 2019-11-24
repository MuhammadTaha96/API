using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public string ISBN_No { get; set; }
        public bool IsDeleted { get; set; }
        public string ImagePath { get; set; }
        public string Edition { get; set; }

        public Publisher Publisher { get; set; }
        public Category Category { get; set; }

        public List<Author> Authors { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Copy> Copies { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
