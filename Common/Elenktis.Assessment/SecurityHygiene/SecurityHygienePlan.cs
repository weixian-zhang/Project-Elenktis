using System;

namespace Elenktis.Assessment.SecurityHygiene
{
    public class SecurityHygienePlan : AssessmentPlan
    {
        public SecurityHygienePlan(string subscriptionId) : base(subscriptionId) {}

        public VMAntimalwareInstalledPolicy VMAntimalwareInstalled { get; set; }

        
    }
}