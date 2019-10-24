using System;
using System.Text;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class AssessAckCommand : IMessage
    {
        public string CorrelationId { get; set; }

        public DateTime TimeReceivedAtHandler { get; set; }

        public bool ToFix { get; set; } = false;

        public string SubscriptionId { get; set; }

        public string AffectedResourceType { get; set; } = "";

        public string AffectedResourceId { get; set; } = "";

        public string Policy { get; set; }

        public string PolicyValue { get; set; }

        public bool IncurCost { get; set; } = false;

        public string ActivityPerformed { get; set; }

        public void AddActivity(string activity)
        {
            var strBuilder = new StringBuilder(ActivityPerformed);
            strBuilder.Append(activity);
            strBuilder.AppendLine();
            ActivityPerformed += strBuilder.ToString();
        }

        public void SetAcknowledge
            (string subscriptionId, string correlationId,
             string policy, string policyValue, string affectedResourceType,
             string affectedResourceId, bool incurCost,
             DateTime timeReceivedAtHandler)
        {
            SubscriptionId = subscriptionId;
            CorrelationId = correlationId;
            Policy = policy;
            PolicyValue = policyValue;
            AffectedResourceType = affectedResourceType;
            AffectedResourceId = affectedResourceId;
            IncurCost = incurCost;
            TimeReceivedAtHandler = timeReceivedAtHandler;
        }

        public void SetToFix()
        {
            ToFix = true;
        }
    }
}