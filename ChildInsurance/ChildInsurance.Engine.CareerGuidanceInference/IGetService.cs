using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChildInsurance.Engine.CareerGuidanceInference
{
    [ServiceContract]
    public interface IGetService
    {
        [OperationContract]
        object GetMethod(object parameter);
    }
}
