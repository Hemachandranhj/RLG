﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChildInsurance.Web.FinancialRecommendationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FinancialRecommendationService.IFinancialRecommendationService")]
    public interface IFinancialRecommendationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFinancialRecommendationService/FutureCost", ReplyAction="http://tempuri.org/IFinancialRecommendationService/FutureCostResponse")]
        decimal FutureCost(decimal actualCost, decimal interestRate, double yearCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFinancialRecommendationService/FutureCost", ReplyAction="http://tempuri.org/IFinancialRecommendationService/FutureCostResponse")]
        System.Threading.Tasks.Task<decimal> FutureCostAsync(decimal actualCost, decimal interestRate, double yearCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFinancialRecommendationService/YearlyContribution", ReplyAction="http://tempuri.org/IFinancialRecommendationService/YearlyContributionResponse")]
        decimal YearlyContribution(decimal futureCost, decimal interestRate, double yearCount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFinancialRecommendationService/YearlyContribution", ReplyAction="http://tempuri.org/IFinancialRecommendationService/YearlyContributionResponse")]
        System.Threading.Tasks.Task<decimal> YearlyContributionAsync(decimal futureCost, decimal interestRate, double yearCount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFinancialRecommendationServiceChannel : ChildInsurance.Web.FinancialRecommendationService.IFinancialRecommendationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FinancialRecommendationServiceClient : System.ServiceModel.ClientBase<ChildInsurance.Web.FinancialRecommendationService.IFinancialRecommendationService>, ChildInsurance.Web.FinancialRecommendationService.IFinancialRecommendationService {
        
        public FinancialRecommendationServiceClient() {
        }
        
        public FinancialRecommendationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FinancialRecommendationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FinancialRecommendationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FinancialRecommendationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public decimal FutureCost(decimal actualCost, decimal interestRate, double yearCount) {
            return base.Channel.FutureCost(actualCost, interestRate, yearCount);
        }
        
        public System.Threading.Tasks.Task<decimal> FutureCostAsync(decimal actualCost, decimal interestRate, double yearCount) {
            return base.Channel.FutureCostAsync(actualCost, interestRate, yearCount);
        }
        
        public decimal YearlyContribution(decimal futureCost, decimal interestRate, double yearCount) {
            return base.Channel.YearlyContribution(futureCost, interestRate, yearCount);
        }
        
        public System.Threading.Tasks.Task<decimal> YearlyContributionAsync(decimal futureCost, decimal interestRate, double yearCount) {
            return base.Channel.YearlyContributionAsync(futureCost, interestRate, yearCount);
        }
    }
}
