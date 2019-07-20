using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class CreateDefaultLogAnalyticsWorkspacePolicy : Policy
    {
        public CreateDefaultLogAnalyticsWorkspacePolicy
            (DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }
    }
}