using System;

namespace Elenktis.Policy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PlanAttribute : Attribute
    {
        public PlanAttribute(Type assessmentPlanType)
        {
            _assessmentPlanType = assessmentPlanType;
        }

        private Type _assessmentPlanType;

        public Type AssessmenPlanType { get {return _assessmentPlanType; } }
    }
}