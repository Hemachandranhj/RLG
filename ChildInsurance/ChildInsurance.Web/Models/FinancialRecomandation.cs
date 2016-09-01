using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChildInsurance.Web.Models
{
    public class FinancialRecomandation
    {
        public int ActualCost { get; set; }

        public int YearDifference { get; set; }

        public int ProjectedCost { get; set; }

        public int ShortFallCost { get; set; }
    }
}