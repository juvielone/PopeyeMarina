using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Models
{
    public class Sailboat : Boat
    {
        public decimal KeelDepth { get; set; }
        public int NumberOfSails { get; set; }
        public string? MotorType { get; set; }
    }
}
