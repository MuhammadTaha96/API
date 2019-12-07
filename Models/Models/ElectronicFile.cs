using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("ElectronicFile")]
    public class ElectronicFile
    {
        [Key]
        public int ElectronicFileId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public ElectronicFileType FileType { get; set; }
    }
}
