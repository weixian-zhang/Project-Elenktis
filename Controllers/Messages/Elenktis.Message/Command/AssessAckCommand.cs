using System;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class AssessAckCommand : IMessage
    {
        public string CorrelationId { get; set; }

        public DateTime TimeCommandReceivedAtHandler { get; set; }

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
            ActivityPerformed.Append(activity);
        }

        public void SetAcknowledge
            (string policy, string policyValue, string affectedResourceType,
            string affectedResourceId, bool incurCost)
        {
            Policy = policy;
            PolicyValue = policyValue;
            AffectedResourceType = affectedResourceType;
            AffectedResourceId = affectedResourceId;
            IncurCost = incurCost;
        }

        public void SetToFix()
        {
            ToFix = true;
        }
    }
}