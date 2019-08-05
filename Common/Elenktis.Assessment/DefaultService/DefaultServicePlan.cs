using System;

namespace Elenktis.Assessment.DefaultService
{
    public class DefaultServicePlan : AssessmentPlan
    {
        public DefaultServicePlan(string subscriptionId) : base(subscriptionId) {}
        
        public CreateDefaultLogAnalyticsWorkspacePolicy
            CreateDefaultLogAnalyticsWorkspacePolicy { get; set; }

        public ASCUpgradeStandardTierPolicy ASCUpgradeStandardTierPolicy { get; set; }

        public ASCAutoRegisterVMEnabledPolicy ASCAutoRegisterVMEnabledPolicy { get; set; }

        public AssociateASCToDefaultLAWorkspacePolicy
            AssociateASCToDefaultLAWorkspacePolicy { get; set; }
    }
}
