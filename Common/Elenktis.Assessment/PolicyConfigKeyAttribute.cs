using System;

namespace Elenktis.Assessment
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PolicyConfigKey : Attribute
    {
        public PolicyConfigKey(string assessmentPlan)
        {
            _assessmentPlan = assessmentPlan;
        }

        private string _assessmentPlan;
    }
}