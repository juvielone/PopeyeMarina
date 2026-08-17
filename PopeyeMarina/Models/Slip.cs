using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Models
{
    public class Slip
    {
        public int SlipID { get; set; }
        public decimal Width { get; set; }
        public decimal SlipLength { get; set; }
        public int DockID { get; set; }
        public bool IsCovered { get; set; }

    }

    public class CoveredSlip : Slip
    {
        public decimal Height { get; set; }
        public string DoorType { get; set; }
    }
}
