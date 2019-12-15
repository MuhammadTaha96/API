using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("ElectronicFileType")]
    public class ElectronicFileType
    {
        [Key]
        public int ElectronicFileTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
