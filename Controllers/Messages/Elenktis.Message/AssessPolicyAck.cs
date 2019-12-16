using System;
using System.Text;

namespace Elenktis.Message
{
    public class AssessPolicyAck
    {
        public Guid CorrelationId { get; set; }

        public DateTime TimeSentAtHandler { get; set; }

        public bool ToFix { get; set; } = false;

        public string SubscriptionId { get; set; }

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

        public void SetAcknowledge
            (string subscriptionId, Guid correlationId,
             string affectedResourceType,
             string affectedResourceId, bool incurCost,
             DateTime timeReceivedAtHandler)
        {
            SubscriptionId = subscriptionId;
            CorrelationId = CorrelationId;
            AffectedResourceType = affectedResourceType;
            AffectedResourceId = affectedResourceId;
            IncurCost = incurCost;
            TimeSentAtHandler = timeReceivedAtHandler;
        }

        public void SetToFix()
        {
            ToFix = true;
        }
    }
}