using System;
using System.Reflection;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Elenktis.Secret;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            await InitAsync();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    services.AddHostedService<MessageBusListenerService>();
                });

            await builder.RunConsoleAsync();
        }

        private async static Task InitAsync()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<DSSpySecret>();

            EndpointConfiguration busConfig = ASBConfigFactory.Create
                (QueueDirectory.Spy.DefaultService,
                 _secrets.ServiceBusConnectionString,
                 ControllerUri.DefaultServiceSpy
                 );
            
            busConfig.RegisterComponents(config => {

                config.ConfigureComponent<IPlanQueryManager>(cf => 
                {
                    return new PlanQueryManager(new PolicySecret()
                        {
                            EtcdHost  = _secrets.EtcdHost,
                            EtcdPort = _secrets.EtcdPort
                        });
                    
                }, DependencyLifecycle.SingleInstance);

                config.ConfigureComponent<IAzure>(cf => 
                {
                    return AzureRMFactory.AuthAndCreateInstance
                        (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);
                    
                }, DependencyLifecycle.SingleInstance);
            });
            
            _endpointInstance = await Endpoint.Start(busConfig);
        }

        private static IEndpointInstance _endpointInstance;

        private static DSSpySecret _secrets;

    }
}
