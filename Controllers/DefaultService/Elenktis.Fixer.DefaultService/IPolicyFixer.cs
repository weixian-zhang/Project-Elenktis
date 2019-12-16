using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Policy;

namespace Elenktis.Fixer.DefaultService
{
    public interface IPolicyFixer<TPolicyStartMsg, TPolicyCompleteMsg>
    {
        void SetCurrentAzureSubscriptionId(string subscriptionId, IAzure azureManager);

        Task<TPolicyCompleteMsg> AssessPolicy
            (TPolicyStartMsg policyStartMsg, IAzure azureManager, IPlanQueryManager policyQueryManager);
        
        Task FixPolicy(TPolicyCompleteMsg policyCompleteMsg, IAzure azureManager, IPlanQueryManager policyQueryManager);
    }
}