namespace Elenktis.Policy
{
    public class AssessmentPlan 
    {
        public AssessmentPlan(string subscriptionId)
        {
            _subscriptionId = subscriptionId;
        }

        private string _subscriptionId;
        public string TenantSubscriptionId { get {return _subscriptionId; }  }

        
    }
}