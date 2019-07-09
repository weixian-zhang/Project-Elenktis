using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCAutoRegisterVMEnabledPolicy : Policy
    {
        public ASCAutoRegisterVMEnabledPolicy(DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }

        [PolicyMeasure]
        public bool Assess { get; set; } = true;

        [PolicyMeasure]
        public bool Remediate { get; set; } = true;

        [PolicyMeasure]
        public bool Ignore { get; set; } = false;
    }
}