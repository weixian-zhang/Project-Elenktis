using System;

namespace Elenktis.Assessment.DefaultService
{
    [Plan(typeof(DefaultServicePlan))]
    public class CheckASCIsStandardTierPolicy : Policy
    {
        public CheckASCIsStandardTierPolicy()
        {

        }
    }
}