using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChildInsurance.Web.Models
{
    public class IllustrationViewModel
    {
        public int Age { get; set; }
        public int Term { get; set; }
        public string Mode { get; set; }
        public int Premium { get; set; }
        public int SumAssured { get; set; }

    }
}