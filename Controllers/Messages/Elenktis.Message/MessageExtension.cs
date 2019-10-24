using System;
using System.Text;

namespace Elenktis.Message
{
    public static class MessageExtension
    {
        public static string GenNewCorrelationId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}