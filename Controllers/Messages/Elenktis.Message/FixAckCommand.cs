using System.Collections.Generic;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class FixAckCommand  : IMessage
    {
        public bool ToFix { get; set; }

        public bool Remediated { get; set; }

        public string SubscriptionId { get; set; }

        public string Policy { get; set; }

        public string PolicyValue { get; set; }

        public string AffectedResourceType { get; set; }

        public string AffectedResourceId { get; set; }

        public bool IncurCost { get; set; }

        public string Activities { get; set; }
    }
}