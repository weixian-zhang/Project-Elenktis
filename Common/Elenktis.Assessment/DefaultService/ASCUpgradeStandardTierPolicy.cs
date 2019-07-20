using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCUpgradeStandardTierPolicy : Policy
    {
        public ASCUpgradeStandardTierPolicy(DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }
    }
}