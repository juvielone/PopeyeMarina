using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopeyeMarina.Core.Config;

namespace PopeyeMarina.Core.Models
{
    public class DailyLease : Lease
    {
        public int NumberOfDays { get; set; }

        public override decimal CalculateFee(Slip slip)
        {
            decimal fee = slip.SlipLength * RateConfig.DailyRatePerMetre * NumberOfDays;

            if (slip is CoveredSlip)
            {
                fee += RateConfig.CoveredSurcharge;
            }

            return fee;
        }
    }
}
