using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildInsurance.Engine.FinancialRecommendation
{
    public class AspirationCost
    {
        public decimal FutureCost(decimal actualCost, decimal interestRate, double yearCount)
        {
            return Math.Round(actualCost * (decimal)Math.Pow((double)(1 + (interestRate / 100)), yearCount), 0);
        }
    }

}
