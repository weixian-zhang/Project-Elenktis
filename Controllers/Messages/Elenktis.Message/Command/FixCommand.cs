using System;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class FixCommand : ICommand
    {
        public string CorrelationId { get; set; }
        
        public string SubscriptionId { get; set; }

        public DateTime TimeReceivedAtHandler { get; set; }
    }
}