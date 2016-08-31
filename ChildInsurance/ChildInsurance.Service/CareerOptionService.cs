using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ChildInsurance.Model.Service;

namespace ChildInsurance.Service
{
    public class CareerOptionService : ICareerOptionService
    {
        public IList<string> GetCareerOptions(InterestRequest request)
        {
            return new List<string>();
        }
    }
}
