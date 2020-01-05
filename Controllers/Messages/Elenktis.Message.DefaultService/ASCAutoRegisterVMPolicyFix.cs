using System;
using System.Text;

namespace Elenktis.Message.DefaultService
{
    public class ASCAutoRegisterVMPolicyFix
    {
        public Guid CorrelationId { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime TimeSentToFixerFromTriggerer { get; set; }

        public DateTime TimeReceivedAtFixer { get; set; }

        public DateTime TimeSentToLoggerFromFixer { get; set; }

        public string Controller { get; set; }

        //full uri of Policy name
        public string AssessPolicyName { get; set; }

        //full uri of Policy name
        public string FixPolicyName { get; set; }

        public bool ToAssess { get; set; } = false;

        public bool ToFix { get; set; } = false;

        //prevent PolicyFix from checking setting/state again
        //settings could be anything: "is backup enabled", "logged to App Insights" and etc
        public bool PostAssessToFix { get; set; }

        public string AffectedResourceType { get; set; } = "";

        public string AffectedResourceId { get; set; } = "";

        public bool IncurCost { get; set; } = false;

        public string ActivityPerformed { get; set; }

        public void AddActivity(string activity)
        {
            var strBuilder = new StringBuilder(ActivityPerformed);
            strBuilder.Append(activity);
            strBuilder.AppendLine();
            ActivityPerformed += strBuilder.ToString();
        }

        public void SetToFix()
        {
            ToFix = true;
        }
    }
}