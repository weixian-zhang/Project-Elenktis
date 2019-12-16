using System;

namespace Elenktis.Message
{
    public class AssessPolicy
    {
        public Guid CorrelationId { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime TimeReceivedAtHandler { get; set; }

    }
}