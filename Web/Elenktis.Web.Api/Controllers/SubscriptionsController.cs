using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// using Elenktis.Policy;
// using Elenktis.Policy.DefaultService;
// using Elenktis.PlanQueryManager;

namespace Elenktis.Web.Api.Subscriptions
{
    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        [HttpGet]
        public String GetValue()
        {
            // await _policyStore.GetPolicyAsync<ASCAutoRegisterVMEnabledPolicy>("21214214214");
            return "test";
        }

        // private PlanQueryManager _planQueryManager;
    }
}
