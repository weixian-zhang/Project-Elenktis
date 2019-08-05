using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class ASCAutoRegisterVMEnabledPolicy : Policy
    {
        public ASCAutoRegisterVMEnabledPolicy(DefaultServicePlan plan) : base(plan)
        {

        }
    }
}