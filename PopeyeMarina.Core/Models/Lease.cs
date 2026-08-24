using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Core.Models
{
    public abstract class Lease
    {
        public int LeaseID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Amount { get; set; }
        public string LeaseType { get; set; }
        public int SlipID { get; set; }
        public string StateRegoNo { get; set; }
        public int CustomerID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public abstract decimal CalculateFee(Slip slip);
    }
}
