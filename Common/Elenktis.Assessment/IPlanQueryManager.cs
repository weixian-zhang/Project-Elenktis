using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;

namespace Elenktis.Assessment
{
    public interface IPlanQueryManager
    {
        Task<DefaultServicePlan> GetDefaultServicePlansAsync(string subscriptionId);
            
        //Task<SecurityHygienePlan> GetSecurityHygienePlanAsync(string subscriptionId);

        //Task<LogEnablerPlan> GetLogEnablerPlanAsync(string subscriptionId);

       
    } 
}