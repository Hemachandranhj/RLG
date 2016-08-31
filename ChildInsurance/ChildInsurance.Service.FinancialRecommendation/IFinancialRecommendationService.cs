using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChildInsurance.Service.FinancialRecommendation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFinancialRecommendationService
    {
        [OperationContract]
        decimal FutureCost(decimal actualCost, decimal interestRate, double yearCount);

        [OperationContract]
        decimal YearlyContribution(decimal futureCost, decimal interestRate, double yearCount);
    }
}
