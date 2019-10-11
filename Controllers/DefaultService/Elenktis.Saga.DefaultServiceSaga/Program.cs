using System;
using System.Threading.Tasks;
using Elenktis.MessageBus;
using Elenktis.Secret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Elenktis.Saga.DefaultServiceSaga
{
    class Program
    {
        async static Task Main(string[] args)
        {
            await InitAsync();

            
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    
                    services.AddHostedService<HealthReportService>(sp =>{
                        return new HealthReportService(_bus);
                    });
                });

            await builder.RunConsoleAsync();
        }

        private static async Task InitAsync()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<SagaSecret>();

            var epc = ASBConfigFactory.Create
                (QueueDirectory.Saga.DefaultService, _secrets.ServiceBusConnectionString);
            epc.UsePersistence<MongoPersistence>();

            epc.DefineCriticalErrorAction(
                onCriticalError: async context => {
                    //todo: log to db
                    await context.Stop();
                });

            await Endpoint.Start(epc);
        }

        private static SagaSecret _secrets;
        private static IEndpointInstance _bus;
    }
}
