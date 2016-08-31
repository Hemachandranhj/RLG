using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ChildInsurance.Engine.FinancialRecommendation;
using ChildInsurance.Model.Service;

namespace ChildInsurance.Service    
{
    public class FinancialRecommendationService : IFinancialRecommendationService
    {
        public decimal FutureCost(decimal actualCost, decimal interestRate, double yearCount)
        {
            var aspirationCost = new AspirationCost();
            return aspirationCost.FutureCost(actualCost, interestRate, yearCount);
        }

        public decimal YearlyContribution(decimal futureCost, decimal interestRate, double yearCount)
        {
            var contribution = new Contribution();
            return contribution.YearlyContribution(futureCost, interestRate, yearCount);
        }
    }
}
