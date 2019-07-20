using System;
using Elenktis.Assessment.DefaultService;
using Elenktis.Assessment.LogEnabler;
using Elenktis.Assessment.SecurityHygiene;

namespace Elenktis.Assessment
{
    public interface IPlanManager
    {
        DefaultServiceAssessmentPlan GetDefaultServicePlan();

        SecurityHygieneAssessmentPlan GetSecurityHygienePlan();

        LogEnablerAssessmentPlan GetLogEnablerPlan();
    } 
}