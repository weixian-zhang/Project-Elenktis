using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

using Elenktis.Azure;
using Elenktis.Configuration;
using Elenktis.Assessment;

[assembly: FunctionsStartup(typeof(Elenktis.Spy.Startup))]
namespace Elenktis.Spy {

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddTransient<ISecretHydrator, AKVSecretHydrator>();
            builder.Services.AddTransient<IAzure, AzureManager>();
            // builder.Services.AddTransient<IPlanManager>(sp => {
            //     return new PlanManager()
            // });

            
        }
    }

}