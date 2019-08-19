using System;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Elenktis.Configuration;
using System.Collections.Generic;

namespace Elenktis.Assessment
{
    public sealed class PlanCreationManager : IPlanCreationManager
    {
        public PlanCreationManager()
        {
            SetupDependencies();
        }

        public async Task CreateDefaultServicePlansAsync
            (string subscriptionId, bool overrideExisting)
        {
            if(overrideExisting)
                await CreateDSPInternalAsync(subscriptionId);
            else{
                bool planExist =
                    await _policyStore.IsPlanExistAsync<DefaultServicePlan>(subscriptionId);

                 if(!planExist)
                     await CreateDSPInternalAsync(subscriptionId);
            }
        }

        public async Task<bool> IsPlanExistAsync<TPlan>
            (string subscriptionId) where TPlan : AssessmentPlan
        {
            return await _policyStore.IsPlanExistAsync<TPlan>(subscriptionId);
        }

        public async Task CreateSecurityHygienePlanAsync
            (string subscriptionId, bool overrideExisting)
        {
            if(overrideExisting)
                await CreateSHPInternalAsync(subscriptionId);
            else{
                bool planExist =
                    await _policyStore.IsPlanExistAsync<SecurityHygienePlan>(subscriptionId);

                 if(!planExist)
                     await CreateSHPInternalAsync(subscriptionId);
            }
        }

        public async Task CreateLogEnablerPlanAsync
            (string subscriptionId, bool overrideExisting)
        {
            if(overrideExisting)
                await CreateLEPInternalAsync(subscriptionId);
            else{
                bool planExist =
                    await _policyStore.IsPlanExistAsync<SecurityHygienePlan>(subscriptionId);

                 if(!planExist)
                     await CreateLEPInternalAsync(subscriptionId);
            }
        }

        private async Task CreateDSPInternalAsync(string subscriptionId)
        {
            await _policyStore.CreatePlanExistFlagAsync<DefaultServicePlan>(subscriptionId);

            await _policyStore.CreateOrSetPolicyAsync
                <ASCAutoRegisterVMEnabledPolicy>(subscriptionId, p => p.ToAssess == true);
            
            await _policyStore.CreateOrSetPolicyAsync
                <ASCAutoRegisterVMEnabledPolicy>(subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync
                <ASCUpgradeStandardTierPolicy>(subscriptionId, p => p.ToAssess == true);
            
            await _policyStore.CreateOrSetPolicyAsync
                <ASCUpgradeStandardTierPolicy>(subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync
                <AssociateASCToDefaultLAWorkspacePolicy>(subscriptionId, p => p.ToAssess == true);
            
            await _policyStore.CreateOrSetPolicyAsync
                <AssociateASCToDefaultLAWorkspacePolicy>(subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync
                <CreateLAWorkspacePolicy>(subscriptionId, p => p.ToAssess == true);
            
            await _policyStore.CreateOrSetPolicyAsync
                <CreateLAWorkspacePolicy>(subscriptionId, p => p.ToRemediate == true);
        }

        private async Task CreateSHPInternalAsync(string subscriptionId)
        {
            await _policyStore.CreatePlanExistFlagAsync<SecurityHygienePlan>(subscriptionId);

            //await _policyStore.CreateOrSetPolicyAsync
        }
        

        private async Task CreateLEPInternalAsync(string subscriptionId)
        {

        }

                private void SetupDependencies()
        {
            ISecretHydrator secretHydrator = new TestSecretHydrator();

            var secrets = secretHydrator.Hydrate<ControllerSecret>();

            IPolicyStoreKeyMapper keyMapper = new EtcdKeyMapper();

            _policyStore = new EtcdPolicyStore(new PolicyStoreConnInfo()
            {
                Hostname = secrets.EtcdHost,
                Port = secrets.EtcdPort
            }, keyMapper);
        }

        private IPolicyStore _policyStore;
    }
}