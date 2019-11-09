using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public int Shelf { get; set; }
        public int ShelfRow { get; set; }
        public int ShelfCol { get; set; }
    }
}
