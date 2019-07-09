using System;

namespace Elenktis.Assessment
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PolicyAssessmentPlanAttribute : Attribute
    {
        public PolicyAssessmentPlanAttribute(string assessmentPlanName)
        {
            _assessmentPlanName = assessmentPlanName;
        }

        private string _assessmentPlanName;

        public string AssessmenPlanName { get {return _assessmentPlanName; } }
    }
}