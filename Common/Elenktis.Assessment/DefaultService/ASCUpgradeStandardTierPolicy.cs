using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCUpgradeStandardTierPolicy : Policy
    {
        public ASCUpgradeStandardTierPolicy(DefaultServicePlan plan) : base(plan)
        {

        }
    }
}