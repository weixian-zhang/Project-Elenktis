using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Policy.DefaultService;
using Elenktis.Policy.LogEnabler;
using Elenktis.Policy.SecurityHygiene;

namespace Elenktis.Policy
{
    public interface IPlanCreationManager
    {
        Task CreateDefaultServicePlansAsync(string subscriptionId, bool overrideExisting);
            
        Task CreateSecurityHygienePlanAsync(string subscriptionId, bool overrideExisting);

        Task CreateLogEnablerPlanAsync(string subscriptionId, bool overrideExisting);

        Task<bool> IsPlanExistAsync<TPlan>(string subscriptionId) where TPlan : AssessmentPlan;
    }
}