using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ChildInsurance.Model.Service;

namespace ChildInsurance.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CareerOptionService : ICareerOptionService
    {
        public IList<string> GetCareerOptions(InterestRequest request)
        {
            return new List<string>();
        }
    }
}
