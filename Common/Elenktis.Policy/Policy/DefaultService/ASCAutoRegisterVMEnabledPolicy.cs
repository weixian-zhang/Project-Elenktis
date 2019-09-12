using System;

namespace Elenktis.Policy.DefaultService
{
    [Plan(typeof(DefaultServicePlan))]
    public class ASCAutoRegisterVMEnabledPolicy : Policy
    {
        public ASCAutoRegisterVMEnabledPolicy()
        {

        }
    }
}