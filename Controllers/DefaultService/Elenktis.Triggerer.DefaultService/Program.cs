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
using MassTransit;
using Serilog;
using System.Threading;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Primitives;
using MongoDB.Driver;
using MassTransit.Persistence.MongoDb;
using System.Security.Authentication;
using MassTransit.Saga;

namespace Elenktis.Saga.DefaultServiceSaga
{
    class Program
    {
        async static Task Main(string[] args)
        {
            InitSecret();
            
            await InitMsgBus();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) => {
                    services.AddSingleton<IBusControl>(_bus);

                    services.AddHostedService<TimerService>(sp =>{
                        var azure = AzureRMFactory.AuthAndCreateInstance
                         (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

                        return new TimerService(_bus, azure, _secrets);
                    });
                });

            await builder.RunConsoleAsync();
        }

        private static void InitSecret()
        {
            _secrets = SecretHydratorFactory.Create().Hydrate<TriggererSecret>();
        }

        private static async Task InitMsgBus()
        {
            try
            {
                var planQueryManager = new PlanQueryManager(new PolicySecret()
                        {
                            EtcdHost  = _secrets.EtcdHost,
                            EtcdPort = _secrets.EtcdPort
                        });
                
                var azure = AzureRMFactory.AuthAndCreateInstance
                    (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

                _bus = Bus.Factory.CreateUsingAzureServiceBus(config => {
                        var host = config.Host(new Uri("https://"+_secrets.ASBHost), h =>
                        {
                            h.SharedAccessSignature(s =>
                            {
                                s.KeyName = _secrets.ASBSASKeyName;
                                s.SharedAccessKey = _secrets.ASBSASKeyValue;
                                s.TokenTimeToLive = TimeSpan.FromDays(1);
                                s.TokenScope = TokenScope.Namespace;
                            });
                        });
                });
                
                 _busHandle = await _bus.StartAsync(_cancelToken);
                
        
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);

                await _busHandle.StopAsync();
            }
        }

        private static BusHandle _busHandle;
        private static TriggererSecret _secrets;
        private static IBusControl _bus;
        private static CancellationToken _cancelToken = new CancellationToken();
    }
}
