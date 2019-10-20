using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public string Reviewer { get; set; }
        public Book Book { get; set; }
    }
}
