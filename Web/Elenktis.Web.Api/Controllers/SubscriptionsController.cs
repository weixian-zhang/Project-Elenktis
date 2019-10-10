using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Elenktis.Policy;
using Elenktis.Secret;

namespace Elenktis.Web.Api
{
    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        public SubscriptionsController()
        {
            SetupDependencies();
        }

        [HttpGet]
        public SubscriptionsModel GetSubscriptions()
        {
            SubscriptionsModel subscriptions = new SubscriptionsModel();
            IDictionary<string, string> policyStoreSubscriptions = _policyStore.GetSubscriptions().Result;
            foreach(var sub in policyStoreSubscriptions)
            {
                string[] data = sub.Key.Split('/');
                string subscriptionId = data[1];
                string plan = data[3];
                
                if (subscriptions.Subscriptions.ContainsKey(subscriptionId) == false)
                {
                    subscriptions.Subscriptions.Add(subscriptionId, new Subscription());
                }
                
                if (data.Length == 4)
                {
                    subscriptions.Subscriptions[subscriptionId].Plans[plan].Enabled = sub.Value.ToBool();
                }

                if (data.Length == 8) 
                {
                    string policy = data[5];
                    string measure = data[7];

                    if (subscriptions.Subscriptions[subscriptionId].Plans[plan].Policies.ContainsKey(policy) == false)
                    {
                        subscriptions.Subscriptions[subscriptionId].Plans[plan].Policies.Add(policy, new Policy()); 
                    }

                    subscriptions.Subscriptions[subscriptionId].Plans[plan].Policies[policy].Measures[measure].Enabled = sub.Value.ToBool();
                }
            }
            return subscriptions;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscription([FromBody] SubscriptionModel subscription)
        {
            if (ModelState.IsValid) {
              if (string.IsNullOrEmpty(subscription.policy))
              {
                await _policyStore.UpdateSubscription($"sub/{subscription.subscription}/plan/{subscription.plan}", subscription.enabled);
              }
              else
              {
                await _policyStore.UpdateSubscription($"sub/{subscription.subscription}/plan/{subscription.plan}/policy/{subscription.policy}/measure/{subscription.measure}", subscription.enabled);
              }
              return Ok(subscription);
            }
            else 
            {
              return BadRequest(subscription);
            }
        }

        private void SetupDependencies()
        {
            ISecretHydrator secretHydrator = SecretHydratorFactory.Create();

            var secrets = secretHydrator.Hydrate<ControllerSecret>();

            IPolicyStoreKeyMapper keyMapper = new EtcdKeyMapper();

            _policyStore = new EtcdPolicyStore(new PolicyStoreConnInfo()
            {
                Hostname = secrets.EtcdHost,
                Port = Convert.ToInt32(secrets.EtcdPort)
            }, keyMapper);
        }

        private IPolicyStore _policyStore;
    }
}
