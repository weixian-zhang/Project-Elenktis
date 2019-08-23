using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

using Elenktis.Assessment;
using Elenktis.Azure;
using Elenktis.Configuration;

[assembly: FunctionsStartup(typeof(Elenktis.Recce.Startup))]
namespace Elenktis.Recce {

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder  builder)
        {
            builder.Services.AddTransient<ISubscriptionRecce, SubscriptionRecce>();
            builder.Services.AddTransient<IAzure, AzureManager>();
            builder.Services.AddTransient<IPlanCreationManager, PlanCreationManager>();  
        }
    }

}