namespace Elenktis.Assessment
{
    public class Policy
    {
        public Policy(AssessmentPlan plan)
        {
            _assessmentPlan = plan;
        }

        private AssessmentPlan _assessmentPlan;

        public AssessmentPlan AssessmentPlan { get {return _assessmentPlan; } }

        [PolicyMeasure]
        public bool ToAssess { get; set; } = true;

        [PolicyMeasure()]
        public bool ToRemediate { get; set; } = true;

        [PolicyMeasure()]
        public bool ToNotify { get; set; }
    }
}