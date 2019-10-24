using System;
using System.Text;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class FixAckCommand  : IMessage
    {
        public string CorrelationId { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime TimeReceivedAtHandler { get; set; }
        

        public bool Remediated { get; set; }

        public string ActivityPerformed { get; set; }

        public void AddActivity(string activity)
        {
            var strBuilder = new StringBuilder(ActivityPerformed);
            strBuilder.Append(activity);
            strBuilder.AppendLine();
            ActivityPerformed += strBuilder.ToString();
        }

        public void SetAcknowledge
            (string subscriptionId, string correlationId, DateTime timeReceived)
        {
            SubscriptionId = subscriptionId;
            CorrelationId = correlationId;
            TimeReceivedAtHandler = timeReceived;
        }
        
        public void SetRemediated()
        {
            Remediated = true;
        }
    }
}