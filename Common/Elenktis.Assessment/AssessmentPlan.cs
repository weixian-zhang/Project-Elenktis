namespace Elenktis.Assessment
{
    public class AssessmentPlan 
    {
        public AssessmentPlan(TenantSubscription subscription)
        {
            _subscription = subscription;
        }

        private TenantSubscription _subscription;
        public TenantSubscription TenantSubscription { get {return _subscription; }  }

        
    }
}