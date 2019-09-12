using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Elenktis.Azure;
using Elenktis.Secret;
using Elenktis.Policy;
using Elenktis.MessageBus;

[assembly: WebJobsStartup(typeof(Elenktis.Spy.Startup))]
namespace Elenktis.Spy {

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
             builder.Services.AddTransient<IAzure, AzureManager>();
             builder.Services.AddTransient<IPlanQueryManager, PlanQueryManager>(); 
             builder.Services.AddTransient<IMsgBusSender>(sp =>
                {
                    return new AzSvcBusSender(SecretHydratorFactory.Create());
                }
             );
        }
    }

}