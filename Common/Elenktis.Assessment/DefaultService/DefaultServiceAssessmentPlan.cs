using System;

namespace Elenktis.Assessment.DefaultService
{
    public class DefaultServiceAssessmentPlan : AssessmentPlan
    {
        public CreateDefaultLogAnalyticsWorkspacePolicy
            CreateDefaultLogAnalyticsWorkspacePolicy { get; set; }

        public ASCUpgradeStandardTierPolicy ASCUpgradeStandardTierPolicy { get; set; }

        public ASCAutoRegisterVMEnabledPolicy ASCAutoRegisterVMEnabledPolicy { get; set; }

        public AssociateASCToDefaultLAWorkspacePolicy
            AssociateASCToDefaultLAWorkspacePolicy { get; set; }
    }
}
