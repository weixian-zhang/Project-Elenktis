using System;
using System.Text;

namespace Elenktis.Message
{
    public static class MessageExtension
    {
        public static string Append(this string str, string textToAppend)
        {
            var strBuilder = new StringBuilder(str);
            strBuilder.Append(textToAppend);
            strBuilder.AppendLine();
            return strBuilder.ToString();
        }

        public static string GenNewCorrelationId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}