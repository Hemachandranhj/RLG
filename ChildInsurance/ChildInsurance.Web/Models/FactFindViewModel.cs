using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChildInsurance.Web.Models
{
    public class FactFindViewModel
    {
        public Guid StudentId { get; set; }
        public string Asset { get; set; }
        public string Liablity { get; set; }
        public string Income { get; set; }
        public string Expenditure { get; set; }
    }
}