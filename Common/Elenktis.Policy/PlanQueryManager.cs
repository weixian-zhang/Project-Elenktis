using System;
using Elenktis.Policy.DefaultService;
using Elenktis.Policy.LogEnabler;
using Elenktis.Policy.SecurityHygiene;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Elenktis.Secret;
using System.Collections.Generic;

namespace Elenktis.Policy
{
    public sealed class PlanQueryManager : IPlanQueryManager
    {
        public PlanQueryManager(PolicySecret secrets) 
        { 
            _secrets = secrets;

            SetupDependencies();
        }


        public async Task<DefaultServicePlan>
            GetDefaultServicePlansAsync(string subscriptionId)
        {
            if(_defaultServicePlan != null)
                return _defaultServicePlan;

            _defaultServicePlan = new DefaultServicePlan(subscriptionId);
            
            _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy = 
                await _policyStore.GetPolicyAsync<ASCAutoRegisterVMEnabledPolicy>(subscriptionId);
            
            _policyStore.OnPolicyChanged<ASCAutoRegisterVMEnabledPolicy>
                (   subscriptionId,
                    (p) => p.ToAssess,
                    (changedValue => {
                        _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy.ToAssess =
                            changedValue.ToBool();
                    }));

            _policyStore.OnPolicyChanged<CheckASCIsStandardTierPolicy>
                (   subscriptionId,
                    (p) => p.ToRemediate,
                    (changedValue => {
                        _defaultServicePlan.ASCUpgradeStandardTierPolicy.ToRemediate =
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

        public async Task<LogEnablerPlan> GetLogEnablerPlanAsync(string subscriptionId)
        {
            return await Task.FromResult<LogEnablerPlan>(null);
        }

        public async Task<SecurityHygienePlan> GetSecurityHygienePlanAsync(string subscriptionId)
        {
            return await Task.FromResult<SecurityHygienePlan>(null);
        }

        private void SetupDependencies()
        {
            IPolicyStoreKeyMapper keyMapper = new EtcdKeyMapper();

            _policyStore = new EtcdPolicyStore(new PolicyStoreConnInfo()
            {
                Hostname = _secrets.EtcdHost,
                Port = Convert.ToInt32(_secrets.EtcdPort)
            }, keyMapper);
        }

        private PolicySecret _secrets;
        private IPolicyStore _policyStore;
        private DefaultServicePlan _defaultServicePlan;
        private SecurityHygienePlan _securityHygienePlan;
        private LogEnablerPlan _logEnablerAssessmentPlan;
    }
}