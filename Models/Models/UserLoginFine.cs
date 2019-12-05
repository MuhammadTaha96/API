using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("UserLoginFine")]
    public class UserLoginFine
    {
        [Key]
        public int UserLoginFineId { get; set; }
        public UserLogin Defaulter { get; set; }
        public int Amount { get; set; }
    }
}
