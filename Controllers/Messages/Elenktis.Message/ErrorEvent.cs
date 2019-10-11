using System;
using System.Collections.Generic;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public class ErrorEvent : IEvent
    {
        public ErrorEvent(Exception ex)
        {
            _errorMessage = ex.Message;
            _stacktrace = ex.ToString();
        }

    
        public DateTime TimeCreated { get { return DateTime.Now; } }

        private string _errorMessage;
        public string ErrorMessage { get; set; }

        private string _stacktrace;
        public string StackTrace { get; set; }
    }
}