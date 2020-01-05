using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Policy;

namespace Elenktis.Fixer.DefaultService
{
    public interface IPolicyFixer<TPolicyFixMessage>
    {
        void SetCurrentAzureSubscriptionId(string subscriptionId, IAzure azureManager);

        Task<TPolicyFixMessage> AssessPolicy
            (TPolicyFixMessage policyFixMsg, IAzure azureManager, IPlanQueryManager policyQueryManager);
        
        Task<TPolicyFixMessage> FixPolicy(TPolicyFixMessage policyFixMsg, IAzure azureManager, IPlanQueryManager policyQueryManager);
    }
}