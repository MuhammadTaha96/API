using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("ReservationStatus")]
    public class ReservationStatus
    {
        [Key]
        public int ReservationStatusId { get; set; }
        public string Name { get; set; }
    }
}
