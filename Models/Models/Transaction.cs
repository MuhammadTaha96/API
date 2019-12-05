using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("Transaction")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public Reservation Reservation { get; set; }
        public UserLogin User { get; set; }
        public Copy Copy { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public int Fine { get; set; }
        public int DaysKept { get; set; }
    }
}
