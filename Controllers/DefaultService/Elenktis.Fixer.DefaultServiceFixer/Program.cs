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
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    
                    services.AddTransient<IPlanQueryManager, PlanQueryManager>();

                    services.AddHostedService<MessageBusBackgroundService>();
                });

            await builder.RunConsoleAsync();
        }

        private void Init()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<DSFixerSecret>();

            // var endpoint = new EndpointConfiguration(
            //         QueueDirectory.Fixer.DefaultServiceSaga.StartSagaQueue);
            // );
            // ConfigureMsgBusDependency(endpoint);

            // endpoint.SendFailedMessagesTo(QueueDirectory.ControllerErrorQueue);

            // var transport = endpoint.UseTransport<AzureServiceBusTransport>();
            // transport.ConnectionString(_secrets.ServiceBusConnectionString);

            // var routeConfig = transport.Routing();
            // ConfigureMsgBusRouting(routeConfig);
        }

        private void ConfigureMsgBusDependency(EndpointConfiguration endpoint)
        {
            IAzure azure = AzureRMFactory.AuthAndCreateInstance
                (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

             endpoint.RegisterComponents((reg) => {

                reg.ConfigureComponent<IPlanQueryManager>(r => {
                    return new PlanQueryManager(new PolicySecret()
                        {
                            EtcdHost = _secrets.EtcdHost,
                            EtcdPort = _secrets.EtcdPort
                        });
                },  DependencyLifecycle.SingleInstance);

                reg.ConfigureComponent<IAzure>(r => {
                    return azure;
                }, DependencyLifecycle.SingleInstance);
            });
        }

        // private void ConfigureMsgBusRouting
        //     (NServiceBus.RoutingSettings<AzureServiceBusTransport> routeConfig)
        // {
        //     routeConfig.RouteToEndpoint
        //         (typeof(EnableASCAutoRegisterVMCommand),
        //             QueueDirectory.Fixer.DefaultServiceSaga.StartSagaQueue);
            
        //     routeConfig.RouteToEndpoint
        //         (typeof(EnableASCAutoRegisterVMAck), QueueDirectory.Fixer.DefaultServiceSaga.AckQueue);
        // }
    
        private DSFixerSecret _secrets;
    }
}
