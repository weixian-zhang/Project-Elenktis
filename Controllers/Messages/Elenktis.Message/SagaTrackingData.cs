using System;
using NServiceBus;

namespace Elenktis.Message
{
    public class SagaTrackingData : ContainSagaData
    {
        public virtual string CorrelationId { get; set; }

        public virtual DateTime TimeCreated { get; set; }

        public virtual string SagaName { get; set; }

        public virtual string Policy { get; set; }

        public virtual string PolicyValue { get; set; }

        public virtual string ResourceType { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual SagaActivityHistory[] Activities { get; set; }
    }
}