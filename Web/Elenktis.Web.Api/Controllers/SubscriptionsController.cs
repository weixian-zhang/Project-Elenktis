using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Elenktis.Policy;
using Elenktis.Secret;

namespace Elenktis.Web.Api.Subscriptions
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
        public IDictionary<string, string> GetSubscriptions()
        {
            return _policyStore.GetSubscriptions().Result;
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
