using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elenktis.Assessment
{
    public interface IPolicyStore
    {
        //Task CreatePolicyAsync<TPolicy>(TPolicy policy) where TPolicy : Policy;

        Task<TPolicy> GetPolicyAsync<TPolicy,TAssessmentPlan>
            (string subscriptionId) where TPolicy : Policy where TAssessmentPlan : AssessmentPlan;

        Task SetPolicyAsync<TAssessmentPlan>
            (string subscriptionId, 
             Expression<Action<Policy>> measure, object value)  where TAssessmentPlan : AssessmentPlan;

        void WatchPolicyChange<TPolicy,TAssessmentPlan>
            (string subscriptionId,
                Expression<Func<Policy,object>> measure,
                Action<string> onPolicyChanged)
                where TPolicy : Policy
                where TAssessmentPlan : AssessmentPlan;
    }
}