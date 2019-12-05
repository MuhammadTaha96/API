using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TransactionResponse
    {
        public string ResponseMessage { get; set; }
        public int ReponseCode { get; set; }
        public int Fine { get; set; }
    }
}
