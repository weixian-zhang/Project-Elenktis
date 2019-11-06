using System;
using NServiceBus;

namespace Elenktis.Message
{
    public class AppEvent  : IMessage
    {
        public string Controller { get; set; }

        public object Message { get; set; }
    }
}