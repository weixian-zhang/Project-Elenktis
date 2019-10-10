using System.Collections.Generic;

namespace Elenktis.Web.Api
{
    public class SubscriptionsModel
    {
        public SubscriptionsModel()
        {
            Subscriptions = new Dictionary<string, Subscription>();
        }
        public Dictionary<string, Subscription> Subscriptions { get; set; }
    }

    public class Subscription
    {
        public Subscription()
        {
            Plans = new Dictionary<string, Plan>();
            Plans.Add("DefaultServicePlan", new Plan());
            Plans.Add("LogEnablerPlan", new Plan());
            Plans.Add("SecurityHygienePlan", new Plan());
        }
        // public bool Enabled { get; set; }
        public Dictionary<string, Plan> Plans { get; set; }
    }

    public class Plan
    {
        public Plan()
        {
            Policies = new Dictionary<string, Policy>();
        }
        public bool Enabled { get; set; }
        public Dictionary<string, Policy> Policies { get; set; }
    }

    public class Policy
    {
        public Policy()
        {
            Measures = new Dictionary<string, Measure>();
            Measures.Add("ToAssess", new Measure());
            Measures.Add("ToRemediate", new Measure());
            Measures.Add("ToNotify", new Measure());
        }
        public Dictionary<string, Measure> Measures { get; set; }
    }

    public class Measure
    {
        public bool Enabled { get; set; }
    }

}
