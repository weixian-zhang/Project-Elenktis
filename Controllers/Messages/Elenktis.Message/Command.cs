using System;
using System.Reflection;

namespace Elenktis.Message
{
    public abstract class Command
    {
        public DateTime TimeCreated { get; set; } = DateTime.Now;

        public string SourceApp { get; set; }

        public string Action { get; set; }
    }
}