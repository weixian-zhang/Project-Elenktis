using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;

namespace Elenktis.Assessment
{
    public interface IPlanCreationManager
    {
        Task CreateDefaultServicePlansAsync(string subscriptionId, bool overrideExisting);
            
        Task CreateSecurityHygienePlanAsync(string subscriptionId, bool overrideExisting);

        Task CreateLogEnablerPlanAsync(string subscriptionId, bool overrideExisting);

        Task<bool> IsPlanExistAsync<TPlan>(string subscriptionId) where TPlan : AssessmentPlan;
    }
}