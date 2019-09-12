using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elenktis.Policy
{
    public interface IPolicyStore
    {
        Task<TPolicy> GetPolicyAsync<TPolicy>(string subscriptionId) where TPolicy : Policy;

        Task CreateOrSetPolicyAsync<TPolicy>
            (string subscriptionId, Expression<Func<TPolicy, object>> measure)
            where TPolicy : Policy;

        Task CreatePlanExistFlagAsync<TPlan>
            (string subscriptionId) where TPlan : AssessmentPlan;

        Task<bool> IsPlanExistAsync<TPlan>(string subscriptionId)  where TPlan : AssessmentPlan;

        void OnPolicyChanged<TPolicy>
            (   string subscriptionId,
                Expression<Func<TPolicy,object>> measureToWatchChange,
                Action<string> onPolicyChanged) where TPolicy : Policy;
                
    }
}