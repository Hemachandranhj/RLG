using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChildInsurance.Model.CareerRecommedation;

namespace ChildInsurance.Web.Models
{
    public class CareerOptionViewModel
    {
        public IList<CareerRecommedation> NonAcademicCareerRecommedation { get; set; }

        public IList<CareerRecommedation> AcademicCareerRecommedation { get; set; }
    }
}