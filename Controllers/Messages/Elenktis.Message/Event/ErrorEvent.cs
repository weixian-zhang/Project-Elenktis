using System;
using System.Collections.Generic;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public class ErrorEvent : IMessage
    {
        public ErrorEvent(Exception ex, string controller)
        {
            ErrorMessage = ex.Message;
            StackTrace = ex.ToString();
            Controller = controller;
        }

        public string Controller { get; set; }

        public DateTime TimeCreated { get { return DateTime.Now; } }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }
    }
}