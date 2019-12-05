using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        
        public Copy ReservedCopy { get; set; }
        public UserLogin ReservedBy { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
