using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Models
{
    public abstract class Boat
    {
        public string StateRegoNo { get; set; }
        public decimal BoatLength { get; set; }
        public string Manufacturer { get; set; }
        public int ModelYear { get; set; }
        public string BoatType { get; set; }
        public int CustomerID { get; set; }

    }
}
