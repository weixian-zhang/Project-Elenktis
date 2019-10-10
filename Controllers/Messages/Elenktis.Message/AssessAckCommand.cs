
using System;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class AssessAckCommand : IMessage
    {
        public string SubscriptionId { get; set; }

        public string Policy { get; set; }

        public string PolicyValue { get; set; }

        public string AffectedResourceType { get; set; }

        public string AffectedResourceId { get; set; }

        public bool IncurCost { get; set; }

        public AffectedResourceProperty[] AffectedResourceProperties { get; set; }
    }
}