using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Common.Messaging
{
    public class AzServiceBusSender : IMessageSender
    {
        public AzServiceBusSender(string servicebusConnString, string queueName)
        {
            Bus.Factory.CreateUsingAzureServiceBus(config =>
            {
                var busHost = config.Host(servicebusConnString, sb =>
                {
                   
                    //svcbus.SharedAccessSignature 
                });

                
            });
        }


    }
}
