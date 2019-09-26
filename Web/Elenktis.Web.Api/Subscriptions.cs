using System.Collections.Generic;
using Elenktis.Policy.DefaultService;
using Elenktis.Policy.LogEnablerPlan;
using Elenktis.Policy.SecurityHygienePlan;

namespace Elenktis.Web.Api.Subscription
{
    public class Subscriptions
    {
        public Dictionary<string, Subscription> Subcriptions;
    }

    public class Subscription
    {
        public DefaultServicePlan DefaultServicePlan;
        public LogEnablerPlan LogEnablerPlan;
        public SecurityHygienePlan SecurityHygienePlan;
    }
}
