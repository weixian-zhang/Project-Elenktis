using System;
using Elenktis.Azure;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Elenktis.Fixer.DefaultService.Startup))]
namespace Elenktis.Fixer.DefaultService {

    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
             builder.Services.AddTransient<IAzure, AzureManager>();
             builder.Services.AddTransient<IPlanQueryManager, PlanQueryManager>(); 
            //  builder.Services.AddTransient<IMsgBusReceiver>(sp =>
            //     {
            //         return new AzSvcBusReceiver(SecretHydratorFactory.Create());
            //     }
            //  );
        }
    }

}