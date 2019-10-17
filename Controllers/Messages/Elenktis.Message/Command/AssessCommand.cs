using System;
using System.Reflection;
using System.Text;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class AssessCommand : ICommand
    {
        public string CorrelationId { get; set; }

        public string SubscriptionId { get; set; }
    }
}