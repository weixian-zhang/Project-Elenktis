using System;

namespace Elenktis.Policy.DefaultService
{
    [Plan(typeof(DefaultServicePlan))]
    public class AssociateASCToDefaultLAWorkspacePolicy : Policy
    {
        public AssociateASCToDefaultLAWorkspacePolicy()
        {

        }
    }
}