using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        public int UserLoginId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RFID { get; set; }
        public bool IsActive { get; set; }
        public string ValidationErrorMessage { get; set; }
        public UserType UserType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
