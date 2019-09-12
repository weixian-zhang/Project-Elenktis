using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Policy.DefaultService;
using Elenktis.Policy.LogEnabler;
using Elenktis.Policy.SecurityHygiene;

namespace Elenktis.Policy
{
    public interface IPlanQueryManager
    {
        Task<DefaultServicePlan> GetDefaultServicePlansAsync(string subscriptionId);
            
        //Task<SecurityHygienePlan> GetSecurityHygienePlanAsync(string subscriptionId);

        //Task<LogEnablerPlan> GetLogEnablerPlanAsync(string subscriptionId);

       
    } 
}