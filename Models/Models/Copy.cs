using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Copy")]
    public class Copy
    {
        [Key]
        public int CopyId { get; set; }
        public string RFID { get; set; }
        public Status Status { get; set; }
        public Book Book { get; set; }
        public Location Location { get; set; }
    }
}
