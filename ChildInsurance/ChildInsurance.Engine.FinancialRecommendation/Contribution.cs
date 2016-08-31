using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildInsurance.Engine.FinancialRecommendation
{
    public class Contribution
    {
        public decimal YearlyContribution(decimal futureCost, decimal interestRate, double yearCount)
        {
            var aspirationCost = new AspirationCost();

            decimal totalAmount = aspirationCost.FutureCost(futureCost, interestRate, yearCount);

            decimal totalInterest = totalAmount - futureCost;

            return Math.Round((futureCost - (totalAmount - futureCost)) / (decimal)yearCount, 0);
        }
    }
}
