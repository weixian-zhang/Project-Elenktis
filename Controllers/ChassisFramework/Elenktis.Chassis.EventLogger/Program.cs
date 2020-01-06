using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.MessageBus;
using Elenktis.Secret;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;

namespace Elenktis.Chassis.EventLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try{
                await Init();
                    
                var hostBuilder = new HostBuilder();
                hostBuilder.ConfigureServices((context, services) => {

                    services.AddHostedService<MessageBusListenerService>(sp => {
                        return new MessageBusListenerService();
                    });
                    
                });
            }
            catch(Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        private static async Task Init()
        {
            try
            {
                InitLogger();

                _secrets = SecretHydratorFactory.Create().Hydrate<EventLoggerSecret>();

                var logDb = InitMongoDb();
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
            }
        }

    //private static async Task InitMsgBus()
        // {
        //     try
        //     {
        //         var planQueryManager = new PlanQueryManager(new PolicySecret()
        //                 {
        //                     EtcdHost  = _secrets.EtcdHost,
        //                     EtcdPort = _secrets.EtcdPort
        //                 });
                
        //         var azure = AzureRMFactory.AuthAndCreateInstance
        //             (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

        //         _bus = Bus.Factory.CreateUsingAzureServiceBus(config => {
        //                 var host = config.Host(new Uri("https://"+_secrets.ASBHost), h =>
        //                 {
        //                     h.SharedAccessSignature(s =>
        //                     {
        //                         s.KeyName = _secrets.ASBSASKeyName;
        //                         s.SharedAccessKey = _secrets.ASBSASKeyValue;
        //                         s.TokenTimeToLive = TimeSpan.FromDays(1);
        //                         s.TokenScope = TokenScope.Namespace;
        //                     });
        //                 });
        //         });
                
        //          _busHandle = await _bus.StartAsync(_cancelToken);
                
        
        //     }
        //     catch(Exception ex)
        //     {
        //         Log.Logger.Error(ex, ex.Message);

        //         await _busHandle.StopAsync();
        //     }
        // }
        
        private static IMongoDatabase InitMongoDb()
        {
            try
            {
                string connectionString = _secrets.EventLoggerCosmosMongoDBConnectionString;
               
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                new MongoUrl(connectionString)
                );
                settings.SslSettings = 
                new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);

                return mongoClient.GetDatabase("pccore-eventlogger-dev-mongo");
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
                throw ex;
            }
        }

        private static void InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
                    .WriteTo
                        .Console()
                    .CreateLogger();
        }

        private static EventLoggerSecret _secrets;


        

                // var dsSagaConfig = ASBConfigFactory.Create
                //     (QueueDirectory.EventLogger.DSSagaEvent,
                //     _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

                // dsSagaConfig.DefineCriticalErrorAction(context => {
                //     Log.Error(context.Exception, context.Error);
                //     return Task.FromResult(1);
                // });

                // dsSagaConfig.RegisterComponents(config => {
                //     config.ConfigureComponent<IMongoDatabase>(compFac => {
                //         return logDb;
                //     }, DependencyLifecycle.SingleInstance);

                //     config.ConfigureComponent<LogStrategist>(compFac => {
                //         return new LogStrategist(logDb);
                //     },DependencyLifecycle.SingleInstance);

                //     config.ConfigureComponent<ILogger>(compFac => {
                //         return Log.Logger;
                //     },DependencyLifecycle.SingleInstance);
                // });
                
                // var errorBusConfig = ASBConfigFactory.Create
                //     (QueueDirectory.EventLogger.Error,
                //     _secrets.ServiceBusConnectionString, ControllerUri.EventLogger);

                // await Endpoint.Start(dsSagaConfig);
    }
}
