using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elenktis.Assessment
{
    public interface IPolicyStore
    {
        Task SetPolicyAsync<TPolicy>(TPolicy policy) where TPolicy : Policy;

        Task<TPolicy> GetPolicyAsync<TPolicy>(AssessmentPlan plan)  where TPolicy : Policy;

        void WatchPolicyChange<TPolicy>
            (Expression<Func<TPolicy,object>> policy, Action<string> onValueChanged)  where TPolicy : Policy;
    }
}