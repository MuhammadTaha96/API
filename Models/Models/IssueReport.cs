using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    [Table("IssueReport")]
    public class IssueReport
    {
        [Key]
        public int IssueReportId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Fine { get; set; }
        public Copy Copy { get; set; }
        public bool Returned { get; set; }
    }
}
