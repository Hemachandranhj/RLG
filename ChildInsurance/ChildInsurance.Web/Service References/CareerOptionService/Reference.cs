﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChildInsurance.Web.CareerOptionService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CareerOptionService.ICareerOptionService")]
    public interface ICareerOptionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICareerOptionService/GetNonAcademyCareerOption", ReplyAction="http://tempuri.org/ICareerOptionService/GetNonAcademyCareerOptionResponse")]
        string GetNonAcademyCareerOption();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICareerOptionService/GetNonAcademyCareerOption", ReplyAction="http://tempuri.org/ICareerOptionService/GetNonAcademyCareerOptionResponse")]
        System.Threading.Tasks.Task<string> GetNonAcademyCareerOptionAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICareerOptionServiceChannel : ChildInsurance.Web.CareerOptionService.ICareerOptionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CareerOptionServiceClient : System.ServiceModel.ClientBase<ChildInsurance.Web.CareerOptionService.ICareerOptionService>, ChildInsurance.Web.CareerOptionService.ICareerOptionService {
        
        public CareerOptionServiceClient() {
        }
        
        public CareerOptionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CareerOptionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CareerOptionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CareerOptionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetNonAcademyCareerOption() {
            return base.Channel.GetNonAcademyCareerOption();
        }
        
        public System.Threading.Tasks.Task<string> GetNonAcademyCareerOptionAsync() {
            return base.Channel.GetNonAcademyCareerOptionAsync();
        }
    }
}
