
using System;

namespace Elenktis.Message.DefaultService
{
    public class ASCAutoRegisterVMPolicyStart
    {
        public Guid CorrelationId { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime TimeSentToHandler { get; set; }

    }
}