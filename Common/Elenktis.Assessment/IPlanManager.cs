using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;

namespace Elenktis.Assessment
{
    public interface IPlanManager
    {
        Task<DefaultServicePlan>GetDefaultServicePlansAsync(string subscriptionId);
            
        //SecurityHygienePlan GetSecurityHygienePlan();

        //LogEnablerPlan GetLogEnablerPlan();
    } 
}