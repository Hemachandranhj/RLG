using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChildInsurance.Web.Models
{
    public class FinancialViewModel
    {
        public decimal ActualCost { get; set; }

        public decimal Years { get; set; }

        public decimal ProjectedCost { get; set; }

        public decimal ShortFall { get; set; }
    }
}