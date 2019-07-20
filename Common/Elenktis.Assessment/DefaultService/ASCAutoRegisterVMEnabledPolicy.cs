using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCAutoRegisterVMEnabledPolicy : Policy
    {
        public ASCAutoRegisterVMEnabledPolicy(DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }
    }
}