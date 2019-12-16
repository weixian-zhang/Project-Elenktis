using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.Transports;
using Microsoft.Azure.ServiceBus.Primitives;

namespace Elenktis.MessageBus
{
    public class MassTransitConfigFactory
    {

        public static IBusControl CreateMsgBus
            (string asbHost, string sasKeyNAme, string sasKey)
        {
            var busControl =
                Bus.Factory.CreateUsingAzureServiceBus(config => {
                    var host = config.Host(new Uri(asbHost), h =>
                    {
                        h.SharedAccessSignature(s =>
                        {
                            s.KeyName = sasKeyNAme;
                            s.SharedAccessKey = sasKey;
                            s.TokenTimeToLive = TimeSpan.FromDays(1);
                            s.TokenScope = TokenScope.Namespace;
                        });
                    });
                });
            
            return busControl;
        }
    }
}