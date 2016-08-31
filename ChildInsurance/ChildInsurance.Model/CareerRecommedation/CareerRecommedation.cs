using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildInsurance.Model.CareerRecommedation
{
    public class CareerRecommedation
    {
        public IList<Interest> Interests { get; set; }

        public string CareerOption { get; set; }
    }
}
