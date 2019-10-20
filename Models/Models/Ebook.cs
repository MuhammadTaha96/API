using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("EBook")]
    public class Ebook
    {
        [Key]
        public int EbookId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
    }
}
