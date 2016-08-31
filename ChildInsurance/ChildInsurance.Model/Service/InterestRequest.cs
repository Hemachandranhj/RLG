using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ChildInsurance.Model.Service
{
    [DataContract]
    public class InterestRequest
    {
        [DataMember]
        public IList<string> InterestName { get; set; }
    }
}
