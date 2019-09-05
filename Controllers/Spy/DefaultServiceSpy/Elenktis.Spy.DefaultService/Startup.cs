using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Elenktis.Azure;
using Elenktis.Secret;
using Elenktis.Assessment;

[assembly: WebJobsStartup(typeof(Elenktis.Spy.Startup))]
namespace Elenktis.Spy {

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {

             builder.Services.AddTransient<ISecretHydrator, NetCoreSecretHydrator>();
             builder.Services.AddTransient<IAzure, AzureManager>();
             builder.Services.AddTransient<IPlanQueryManager, PlanQueryManager>();  
        }
    }

}