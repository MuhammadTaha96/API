using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
