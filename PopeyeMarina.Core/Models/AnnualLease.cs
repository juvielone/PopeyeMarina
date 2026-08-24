using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopeyeMarina.Core.Config;

namespace PopeyeMarina.Core.Models
{
    public class AnnualLease : Lease
    {
        public bool PayMonthly { get; set; }
        public decimal BalanceDue { get; set; }

        public override decimal CalculateFee(Slip slip)
        {
            decimal fee = slip.SlipLength * RateConfig.MonthlyRatePerMetre * 12;

            if (slip is CoveredSlip)
            {
                fee += RateConfig.CoveredSurcharge;
            }

            return fee;
        }
    }
}
