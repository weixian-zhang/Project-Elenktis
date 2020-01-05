using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Elenktis.Secret;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.Extensions.Hosting;
namespace Elenktis.Fixer.DefaultService
{
    class Program
    {
        async static Task Main(string[] args)
        {
            InitAsync();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                   services.AddMassTransit(c => {
                       c.AddConsumer<ASCAutoRegisterVMPolicyFixer>();
                   });
                });

            await builder.RunConsoleAsync();
        }

        private async static Task InitAsync()
        {
            var secret = SecretHydratorFactory.Create().Hydrate<DSFixerSecret>();

            _bus = Bus.Factory.CreateUsingAzureServiceBus(config => 
            {
                var host = config.Host(new Uri("https://"+ secret.ASBHost), h =>
                {
                    h.SharedAccessSignature(s =>
                    {
                        s.KeyName = secret.ASBSASKeyName;
                        s.SharedAccessKey = secret.ASBSASKeyValue;
                        s.TokenTimeToLive = TimeSpan.FromDays(1);
                        s.TokenScope = TokenScope.Namespace;
                    });
                });
                
                IPlanQueryManager planQueryManager = new PlanQueryManager(new PolicySecret()
                            {
                                EtcdHost  = secret.EtcdHost,
                                EtcdPort = secret.EtcdPort
                            });
                
                IAzure azure = AzureRMFactory.AuthAndCreateInstance
                    (secret.TenantId, secret.ClientId, secret.ClientSecret);

                config.ReceiveEndpoint
                    (QueueDirectory.Fixer.DefaultService, e => {
                        
                        e.Consumer(() => {
                            return new ASCAutoRegisterVMPolicyFixer(secret, planQueryManager, azure);
                        });
                        
                    });
            });

            await _bus.StartAsync(_cancelToken);
        }

        private static BusHandle _busHandle;

        private static IBusControl _bus;
        private static CancellationToken _cancelToken = new CancellationToken();

    }
}
