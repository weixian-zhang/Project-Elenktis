using System;
using System.Reflection;

namespace Elenktis.Message
{
    public abstract class ExecutionEvent
    {
        public string ActionPerformed { get; set; }

        public DateTime TimeCreated { get; set; }

        public string SourceAppName { get; } = Assembly.GetCallingAssembly().FullName;
    }
}