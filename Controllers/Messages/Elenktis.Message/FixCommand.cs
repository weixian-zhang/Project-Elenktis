using System;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class FixCommand : ICommand
    {
        public bool ToFix { get; set; }
        
        public string SubscriptionId { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public DateTime TimeReceived { get; set; } //by handler
    }
}