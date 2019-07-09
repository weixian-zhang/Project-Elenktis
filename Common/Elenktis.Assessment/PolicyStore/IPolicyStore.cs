using System.Threading.Tasks;

namespace Elenktis.Assessment
{
    public interface IPolicyStore
    {
        Task SetPolicy<T>(T policy) where T : Policy;

        Task<T> GetPolicy<T>()  where T : Policy;

        //Task WatchPolicyChange<T>(T policy) where T : Policy;

        //T GetPoliciesByAssessmentPlan<T>(T plan) where T : AssessmentPlan;
    }
}