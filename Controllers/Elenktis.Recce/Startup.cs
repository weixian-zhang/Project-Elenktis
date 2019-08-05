using System;
using Elenktis.Assessment;
using Elenktis.Azure;
using Elenktis.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Elenktis.Recce.Startup))]
namespace Elenktis.Recce {

    public class Startup : FunctionsStartup
    {
         public override void Configure(IFunctionsHostBuilder builder)
         {
             builder.Services.AddTransient<ISubscriptionRecce, SubscriptionRecce>();
             builder.Services.AddTransient<IPlanManager, PlanManager>();
             builder.Services.AddTransient<IAzure, AzureManager>();
             builder.Services.AddTransient<ISecretHydrator, AKVSecretHydrator>();
         }
    }
}
