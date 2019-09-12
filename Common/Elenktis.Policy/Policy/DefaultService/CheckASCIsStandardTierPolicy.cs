using System;

namespace Elenktis.Policy.DefaultService
{
    [Plan(typeof(DefaultServicePlan))]
    public class CheckASCIsStandardTierPolicy : Policy
    {
        public CheckASCIsStandardTierPolicy()
        {

        }
    }
}