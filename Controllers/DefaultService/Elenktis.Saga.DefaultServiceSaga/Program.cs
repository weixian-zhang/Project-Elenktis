using System;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.Azure;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Elenktis.Secret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using AutoMapper;

namespace Elenktis.Saga.DefaultServiceSaga
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await InitAsync();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    services.AddSingleton<IEndpointInstance>(_bus);

                    services.AddHostedService<TimerService>(sp =>{
                        var azure = AzureRMFactory.AuthAndCreateInstance
                         (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

                        return new TimerService(_bus, azure);
                    });
                });

            await builder.RunConsoleAsync();
        }

        private static async Task InitAsync()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<SagaSecret>();

            IMapper msgMapper = MessageMapper.CreateMapper();

            var epc = ASBConfigFactory.Create
                        (QueueDirectory.Saga.DefaultService,
                        _secrets.ServiceBusConnectionString,
                        ControllerUri.DefaultServiceSaga,
                        _secrets.DSSagaSqlConnectionString);
            
            epc.RegisterComponents(config => {
                
                config.ConfigureComponent<IAzure>(c => {
                    return AzureRMFactory.AuthAndCreateInstance
                        (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);  
                }, DependencyLifecycle.SingleInstance);

                config.ConfigureComponent<IMapper>(c => {
                    return msgMapper;
                }, DependencyLifecycle.SingleInstance);

                config.ConfigureComponent<IPlanQueryManager>(c => {
                   return new PlanQueryManager(new PolicySecret()
                   {
                       EtcdHost = _secrets.EtcdHost,
                       EtcdPort = _secrets.EtcdPort
                   });
                }, DependencyLifecycle.SingleInstance);

            });

            epc.DefineCriticalErrorAction(
                onCriticalError: async context => {
                    //todo: log to saga db
                    await context.Stop();
                });

            _bus = await Endpoint.Start(epc);
        }

        private static SagaSecret _secrets;
        private static IEndpointInstance _bus;
    }
}
