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
    public sealed class PlanManager : IPlanManager
    {
        public PlanManager() 
        { 
            SetupDependencies();
        }


        private async Task CreateDefaultServicePlanAsync(string subscriptionId)
        {
            
        }

        public async Task<DefaultServicePlan>
            GetDefaultServicePlansAsync(string subscriptionId)
        {
            if(_defaultServicePlan != null)
                return _defaultServicePlan;

            _defaultServicePlan = new DefaultServicePlan(subscriptionId);
            
            _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy = 
                await _policyStore.GetPolicyAsync
                    <ASCAutoRegisterVMEnabledPolicy, DefaultServicePlan>(subscriptionId);
            
            _policyStore.
                WatchPolicyChange<ASCAutoRegisterVMEnabledPolicy, DefaultServicePlan>
                (subscriptionId,
                    (p) => p.ToAssess,
                    (changedValue => {
                        _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy.ToAssess =
                            changedValue.ToBool();
                    }));

            _policyStore.
                WatchPolicyChange<ASCAutoRegisterVMEnabledPolicy, DefaultServicePlan>
                (subscriptionId, (p) => p.ToRemediate,
                    (changedValue => {
                        _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy.ToRemediate =
                            changedValue.ToBool();
                    }));

            // ASCUpgradeStandardTierPolicy
            // _defaultServicePlan.ASCUpgradeStandardTierPolicy =
            //     await _policyStore.GetPolicyAsync<ASCUpgradeStandardTierPolicy>(_defaultServicePlan);

            // _policyStore.WatchPolicyChange<ASCUpgradeStandardTierPolicy>((p) => p.ToAssess,
            //     (changedValue => {
            //         _defaultServicePlan.ASCUpgradeStandardTierPolicy.ToAssess = changedValue.ToBool();
            //     }));

            // _policyStore.WatchPolicyChange<ASCUpgradeStandardTierPolicy>((p) => p.ToRemediate,
            //     (changedValue => {
            //         _defaultServicePlan.ASCUpgradeStandardTierPolicy.ToRemediate = changedValue.ToBool();
            //     }));

            // // AssociateASCToDefaultLAWorkspacePolicy
            // _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy =
            //     await _policyStore.GetPolicyAsync<AssociateASCToDefaultLAWorkspacePolicy>(_defaultServicePlan);

            // _policyStore.WatchPolicyChange<AssociateASCToDefaultLAWorkspacePolicy>((p) => p.ToAssess,
            //     (changedValue => {
            //         _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy.ToAssess = changedValue.ToBool();
            //     }));

            // _policyStore.WatchPolicyChange<AssociateASCToDefaultLAWorkspacePolicy>((p) => p.ToRemediate,
            //     (changedValue => {
            //         _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy.ToRemediate = changedValue.ToBool();
            //     }));

            // // CreateDefaultLogAnalyticsWorkspacePolicy
            // _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy =
            //     await _policyStore.GetPolicyAsync<CreateDefaultLogAnalyticsWorkspacePolicy>(_defaultServicePlan);

            // _policyStore.WatchPolicyChange<CreateDefaultLogAnalyticsWorkspacePolicy>((p) => p.ToAssess,
            //     (changedValue => {
            //         _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy.ToAssess = changedValue.ToBool();
            //     }));

            // _policyStore.WatchPolicyChange<CreateDefaultLogAnalyticsWorkspacePolicy>((p) => p.ToRemediate,
            //     (changedValue => {
            //         _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy.ToRemediate = changedValue.ToBool();
            //     }));

            return _defaultServicePlan;
        }

        public LogEnablerPlan GetLogEnablerPlan()
        {
            return _logEnablerAssessmentPlan;
        }

        public SecurityHygienePlan GetSecurityHygienePlan()
        {
            return _securityHygienePlan;
        }

        private void HydrateSecret()
        {
            
        }

        private void SetupDependencies()
        {
            ISecretHydrator secretHydrator = new ManualSecretHydrator();

            var secrets = secretHydrator.Hydrate<ControllerSecret>();

            IPolicyStoreKeyMapper keyMapper = new EtcdKeyMapper();

            _policyStore = new EtcdPolicyStore(new PolicyStoreConnInfo()
            {
                Hostname = secrets.EtcdHost,
                Port = secrets.EtcdPort
            }, keyMapper);
        }

        private IPolicyStore _policyStore;
        private DefaultServicePlan _defaultServicePlan;
        private SecurityHygienePlan _securityHygienePlan;
        private LogEnablerPlan _logEnablerAssessmentPlan;
    }
}