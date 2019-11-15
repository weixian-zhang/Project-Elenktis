using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

using Elenktis.Policy;
using Elenktis.Azure;
using Elenktis.Secret;

[assembly: FunctionsStartup(typeof(Elenktis.Chassis.Seeder.Startup))]
namespace Elenktis.Chassis.Seeder {

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder  builder)
        {
            try
            {
                var secretHydrator = SecretHydratorFactory.Create();
                var secrets = secretHydrator.Hydrate<SeederSecret>();

                builder.Services.AddTransient<ISubscriptionRecce, SubscriptionRecce>();
                
                builder.Services.AddSingleton<SeederSecret>(sp => {return secrets;});

                builder.Services.AddTransient<IPlanCreationManager>(sp => {
                    return new PlanCreationManager(new PolicySecret(){
                        EtcdHost = secrets.EtcdHost,
                        EtcdPort = secrets.EtcdPort
                    });
                });
            }
            catch(Exception ex)
            {
                
            }
        }
    }

}