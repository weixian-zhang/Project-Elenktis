using System;

namespace Elenktis.Assessment
{
    [PolicyAssessmentPlan("DefaultService")]
    public class DefaultServiceAssessPolicy : Policy
    {
        public DefaultServiceAssessPolicy(DefaultServiceAssessmentPlan plan) : base(plan) {}
    }
}