using System;
using System.Reflection;

namespace Elenktis.Message
{
    public abstract class Command
    {
        public int TimeCreated { get; set; }

        public string SourceAppName { get; } = ""; //Assembly.GetCallingAssembly().GetName();
    }
}