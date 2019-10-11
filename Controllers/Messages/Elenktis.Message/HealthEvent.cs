using System;
using NServiceBus;

namespace Elenktis.Message
{
    public class HealthEvent  : ICommand
    {
        public string Controller { get; set; }

        public DateTime TimeCreated { get; set; } = DateTime.Now;
    }
}