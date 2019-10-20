using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Publisher")]
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
