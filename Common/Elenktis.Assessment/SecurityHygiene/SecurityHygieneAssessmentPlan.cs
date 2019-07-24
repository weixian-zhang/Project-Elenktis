using System;

namespace Elenktis.Assessment.SecurityHygiene
{
    public class SecurityHygieneAssessmentPlan : AssessmentPlan
    {
        public SecurityHygieneAssessmentPlan(TenantSubscription subscription) : base(subscription) {}

        public VMAntimalwareInstalledPolicy VMAntimalwareInstalled { get; set; }

        
    }
}