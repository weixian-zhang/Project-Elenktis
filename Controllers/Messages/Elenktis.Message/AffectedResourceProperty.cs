using System;
using NServiceBus;

namespace Elenktis.Message
{
    public class AffectedResourceProperty
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }
    }
}