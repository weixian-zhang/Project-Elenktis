using System;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Elenktis.Configuration;

namespace Elenktis.Assessment
{
    public sealed class PlanManager : IPlanManager
    {
        public PlanManager() 
        { 
            SetupDependencies();
            
            InitializeDefaultServicePlansAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeDefaultServicePlansAsync()
        {
            _defaultServicePlan = new DefaultServiceAssessmentPlan();
            
            //init ASCAutoRegisterVMEnabledPolicy
            _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy = 
                await _policyStore.GetPolicyAsync<ASCAutoRegisterVMEnabledPolicy>();
            
            _policyStore.
                WatchPolicyChange<ASCAutoRegisterVMEnabledPolicy>((p) => p.ToAssess,
                    (changedValue => {
                        _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy.ToAssess =
                            changedValue.ToBool();
                    }));

            _policyStore.
                WatchPolicyChange<ASCAutoRegisterVMEnabledPolicy>((p) => p.ToRemediate,
                    (changedValue => {
                        _defaultServicePlan.ASCAutoRegisterVMEnabledPolicy.ToRemediate =
                            changedValue.ToBool();
                    }));

            // ASCUpgradeStandardTierPolicy
            _defaultServicePlan.ASCUpgradeStandardTierPolicy =
                await _policyStore.GetPolicyAsync<ASCUpgradeStandardTierPolicy>();

            _policyStore.WatchPolicyChange<ASCUpgradeStandardTierPolicy>((p) => p.ToAssess,
                (changedValue => {
                    _defaultServicePlan.ASCUpgradeStandardTierPolicy.ToAssess = changedValue.ToBool();
                }));

            _policyStore.WatchPolicyChange<ASCUpgradeStandardTierPolicy>((p) => p.ToRemediate,
                (changedValue => {
                    _defaultServicePlan.ASCUpgradeStandardTierPolicy.ToRemediate = changedValue.ToBool();
                }));

            // AssociateASCToDefaultLAWorkspacePolicy
            _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy =
                await _policyStore.GetPolicyAsync<AssociateASCToDefaultLAWorkspacePolicy>();

            _policyStore.WatchPolicyChange<AssociateASCToDefaultLAWorkspacePolicy>((p) => p.ToAssess,
                (changedValue => {
                    _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy.ToAssess = changedValue.ToBool();
                }));

            _policyStore.WatchPolicyChange<AssociateASCToDefaultLAWorkspacePolicy>((p) => p.ToRemediate,
                (changedValue => {
                    _defaultServicePlan.AssociateASCToDefaultLAWorkspacePolicy.ToRemediate = changedValue.ToBool();
                }));

            // CreateDefaultLogAnalyticsWorkspacePolicy
            _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy =
                await _policyStore.GetPolicyAsync<CreateDefaultLogAnalyticsWorkspacePolicy>();

            _policyStore.WatchPolicyChange<CreateDefaultLogAnalyticsWorkspacePolicy>((p) => p.ToAssess,
                (changedValue => {
                    _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy.ToAssess = changedValue.ToBool();
                }));

            _policyStore.WatchPolicyChange<CreateDefaultLogAnalyticsWorkspacePolicy>((p) => p.ToRemediate,
                (changedValue => {
                    _defaultServicePlan.CreateDefaultLogAnalyticsWorkspacePolicy.ToRemediate = changedValue.ToBool();
                }));
        }

        public DefaultServiceAssessmentPlan GetDefaultServicePlan()
        {
            return _defaultServicePlan;
        }

        public LogEnablerAssessmentPlan GetLogEnablerPlan()
        {
            return _logEnablerAssessmentPlan;
        }

        public SecurityHygieneAssessmentPlan GetSecurityHygienePlan()
        {
            return _securityHygienePlan;
        }

        private void HydrateSecret()
        {
            
        }

        private void SetupDependencies()
        {
            ISecretHydrator secretHydrator = new AKVSecretHydrator();
            ControllerSecret secrets = secretHydrator.Hydrate<ControllerSecret>();

            var svcCollection = new ServiceCollection();

            svcCollection.AddTransient<IPolicyStoreKeyMapper, PolicyStoreEtcdKeyMapper>();
        
            svcCollection.AddTransient<IPolicyStore>((sp) => 
            {
                return new EtcdPolicyStore
                    (secrets.EtcdHost, secrets.EtcdPort, new PolicyStoreEtcdKeyMapper());
            });

            ServiceProvider svcProvider = svcCollection.BuildServiceProvider();

            _policyStore = svcProvider.GetService<IPolicyStore>();
        }

        private ControllerSecret  _secrets;
        private IPolicyStore _policyStore;
        private DefaultServiceAssessmentPlan _defaultServicePlan;
        private SecurityHygieneAssessmentPlan _securityHygienePlan;
        private LogEnablerAssessmentPlan _logEnablerAssessmentPlan;
    }
}