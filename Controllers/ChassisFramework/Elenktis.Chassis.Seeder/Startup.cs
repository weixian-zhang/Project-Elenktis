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
            builder.Services.AddTransient<ISubscriptionRecce, SubscriptionRecce>();
            builder.Services.AddTransient<IPlanCreationManager, PlanCreationManager>();  
        }
    }

}