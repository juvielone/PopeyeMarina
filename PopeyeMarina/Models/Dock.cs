using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Models
{
    public class Dock
    {
        public int DockID { get; set; }
        public string Location { get; set; }
        public bool HasElectricity { get; set; }
        public bool HasWater { get; set; }
    }
}
