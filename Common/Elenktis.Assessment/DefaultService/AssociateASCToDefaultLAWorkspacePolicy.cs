using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class AssociateASCToDefaultLAWorkspacePolicy : Policy
    {
        public AssociateASCToDefaultLAWorkspacePolicy(DefaultServiceAssessmentPlan plan) : base(plan)
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