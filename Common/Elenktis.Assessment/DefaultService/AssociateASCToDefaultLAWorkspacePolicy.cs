using System;

namespace Elenktis.Assessment.DefaultService
{
    [PolicyAssessmentPlan("DefaultService")]
    public class AssociateASCToDefaultLAWorkspacePolicy : Policy
    {
        public AssociateASCToDefaultLAWorkspacePolicy(DefaultServiceAssessmentPlan plan) : base(plan)
        {

        }
    }
}