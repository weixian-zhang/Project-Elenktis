using System;

namespace Elenktis.Message
{
    public class FixPolicy
    {
        public Guid CorrelationId { get; set; }
        
        public string SubscriptionId { get; set; }

        public DateTime TimeReceivedAtHandler { get; set; }
    }
}