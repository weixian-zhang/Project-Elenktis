using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCUpgradeStandardTierPolicy : Policy
    {
        public ASCUpgradeStandardTierPolicy(DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }

        [PolicyMeasure]
        public bool Assess { get; set; }
        
        [PolicyMeasure]
        public bool Remediate { get; set; }
        
        [PolicyMeasure]
        public bool Ignore { get; set; }
    }
}