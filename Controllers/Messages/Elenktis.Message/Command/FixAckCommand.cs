using System;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class FixAckCommand  : IMessage
    {
        public string CorrelationId { get; set; }

        public DateTime TimeCommandReceivedAtHandler { get; set; }
        

        public bool Remediated { get; set; }

        public string ActivityPerformed { get; set; }

        public void AddActivity(string activity)
        {
            ActivityPerformed.Append(activity);
        }
        
        public void SetRemediated()
        {
            Remediated = true;
        }
    }
}