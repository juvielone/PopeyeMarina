using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Core.Models
{
    public class Powerboat : Boat
    {
        public int NumberOfEngines  { get; set; }
        public string FuelType { get; set; }
    }
}
