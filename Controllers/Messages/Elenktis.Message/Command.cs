using System;
using System.Reflection;

namespace Elenktis.Message
{
    public abstract class Command
    {
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public string Action { get; set; }

        public string SubscriptionId { get; set; }

        public bool IncurCost { get; set; }

        public bool EmailNotifyOnCostIncurred { get; set; }
    }
}