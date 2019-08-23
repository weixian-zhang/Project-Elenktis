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

        public async Task CreateSecurityHygienePlanAsync
            (string subscriptionId, bool overrideExisting)
        {
            if(overrideExisting)
                await CreateSHPInternalAsync(subscriptionId);
            else
            {
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
            else
            {
                bool planExist =
                    await _policyStore.IsPlanExistAsync<SecurityHygienePlan>(subscriptionId);

                 if(!planExist)
                     await CreateLEPInternalAsync(subscriptionId);
            }
        }

        public async Task<bool> IsPlanExistAsync<TPlan>
            (string subscriptionId) where TPlan : AssessmentPlan
        {
            return await _policyStore.IsPlanExistAsync<TPlan>(subscriptionId);
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

            await _policyStore.CreateOrSetPolicyAsync
                <EnableAzBackupOnVMPolicy>(subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync
                <EnableAzBackupOnVMPolicy>(subscriptionId, p => p.ToRemediate == true);
        }

        private async Task CreateSHPInternalAsync(string subscriptionId)
        {
            await _policyStore.CreatePlanExistFlagAsync<SecurityHygienePlan>(subscriptionId);

            await _policyStore.CreateOrSetPolicyAsync<CheckAKVImportedCertExpiryPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckAKVImportedCertExpiryPolicy>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckAppServiceImportedCertExpiry>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckAppServiceImportedCertExpiry>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckDDoSStandardEnableOnVNetWithPIPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckDDoSStandardEnableOnVNetWithPIPPolicy>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckRemoteDebugEnableOnAppServicePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckRemoteDebugEnableOnAppServicePolicy>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckRemoteDebugEnableOnAzFuncPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckRemoteDebugEnableOnAzFuncPolicy>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckSubnetWithMissingNSGPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<CheckSubnetWithMissingNSGPolicy>
                (subscriptionId, p => p.ToNotify == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAuditOnAzSQLPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAuditOnAzSQLPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzPostgreSQLATPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzPostgreSQLATPPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzSQLATPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzSQLATPPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzSQLTDEPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzSQLTDEPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzStorageATPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableAzStorageATPPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableHttpsOnlyOnAppServicePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableHttpsOnlyOnAppServicePolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableMariaDbATPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableMariaDbATPPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableMySQLATPPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableMySQLATPPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableResourceLockAtSubscriptionPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableResourceLockAtSubscriptionPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableSecureTransferOnAzStoragePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableSecureTransferOnAzStoragePolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVAScanOnAzSQLPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVAScanOnAzSQLPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVMConnectedDefaultLAWorkspacePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVMConnectedDefaultLAWorkspacePolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVMDiskEncryptionPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableVMDiskEncryptionPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<InstallMissingWinDefenderOnVMPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<InstallMissingWinDefenderOnVMPolicy>
                (subscriptionId, p => p.ToRemediate == true);
        }
        

        private async Task CreateLEPInternalAsync(string subscriptionId)
        {
           await _policyStore.CreatePlanExistFlagAsync<LogEnablerPlan>(subscriptionId);

           await _policyStore.CreateOrSetPolicyAsync<AddMonitorSolutionPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<AddMonitorSolutionPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableDiagSettingsOnPaaSPolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<EnableDiagSettingsOnPaaSPolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<LinkActivityLogToLAWorkspacePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<LinkActivityLogToLAWorkspacePolicy>
                (subscriptionId, p => p.ToRemediate == true);

            await _policyStore.CreateOrSetPolicyAsync<LinkAzAutomationToLAWorkspacePolicy>
                (subscriptionId, p => p.ToAssess == true);

            await _policyStore.CreateOrSetPolicyAsync<LinkAzAutomationToLAWorkspacePolicy>
                (subscriptionId, p => p.ToRemediate == true);
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