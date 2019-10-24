using System;
using System.Threading;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;
using Elenktis.Secret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Fixer.DefaultServiceFixer
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await InitAsync();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    services.AddHostedService<MessageBusListenerService>();
                    services.AddHostedService<HealthReportService>(sp =>{
                        return new HealthReportService(_endpointInstance);
                    });
                });

            await builder.RunConsoleAsync();
        }

        private async static Task InitAsync()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<DSFixerSecret>();

            EndpointConfiguration busConfig = ASBConfigFactory.Create
                (QueueDirectory.Fixer.DefaultService,
                 _secrets.ServiceBusConnectionString,
                 ControllerUri.DefaultServiceFixer);
            
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
    
        private static DSFixerSecret _secrets;
    }
}
