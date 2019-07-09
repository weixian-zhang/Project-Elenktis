namespace Elenktis.Assessment
{
    public class Policy
    {
        public Policy(AssessmentPlan plan)
        {
            _assessmentPlan = plan;
        }

        private AssessmentPlan _assessmentPlan;

        public AssessmentPlan AssessmentPlan { get; }
    }
}