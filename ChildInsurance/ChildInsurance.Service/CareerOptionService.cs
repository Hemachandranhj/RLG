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
        public string GetNonAcademyCareerOption(InterestRequest request)
        {
            var careerEngine = new Carreer.CareerRecommendation();
            var career = careerEngine.GetNonAcademyCareerOption(request);
            return career;
        }
    }
}
