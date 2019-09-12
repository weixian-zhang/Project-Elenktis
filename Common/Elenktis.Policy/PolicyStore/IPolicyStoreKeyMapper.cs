using System.Collections.Generic;
using System.Reflection;

namespace Elenktis.Policy
{
    public interface IPolicyStoreKeyMapper
    {
        IEnumerable<PolicyKeyMeasureMap> GetKeyMeasureMap<T>
            (string subscriptionId, T policy) where T : Policy;

        string CreatePolicyStoreKey
            (string subscriptionId, string assessmentPlanName, string policyName, string measureName);
        
        string CreatePlanKey<TPlan>(string subscriptionId) where TPlan : AssessmentPlan;
        
        string GetPlanNameByAttribute<TPolicy>() where TPolicy : Policy;
    }
}