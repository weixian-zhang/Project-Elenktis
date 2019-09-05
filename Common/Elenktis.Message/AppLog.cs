using System;
using System.Reflection;

namespace Elenktis.Message
{
    public class AppLog
    {
        public Exception Exception { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool IsError { get; set; }

        public string Message { get; set; }

        public string SourceAppName { get; set; }

        public AppLog Create(string message) => new AppLog()
        {
            TimeCreated = DateTime.Now,
            IsError = false,
            Message = message,
            SourceAppName = GetCallingAppName()
        };

        public AppLog Create(Exception exception)
        {
            return new AppLog()
            {
                Exception = exception,
                TimeCreated = DateTime.Now,
                IsError = true,
                Message = exception.Message,
                SourceAppName = GetCallingAppName()
            };
        }

        public AppLog Create(string message, Exception exception)
        {
            return new AppLog()
            {
                Exception = exception,
                TimeCreated = DateTime.Now,
                IsError = true,
                Message = message,
                SourceAppName = GetCallingAppName()
            };
        }
    
        private string GetCallingAppName()
        {
            return ""; //Assembly.GetCallingAssembly().GetName();
        }
    }
}