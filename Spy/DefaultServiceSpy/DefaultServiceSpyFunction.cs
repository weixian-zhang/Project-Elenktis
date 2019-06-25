using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public static class DefaultServiceSpyFunction
    {
        [FunctionName("DefaultServiceSpyFunction")]
        public static void Run([TimerTrigger("*/10 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            //https://medium.com/@pjbgf/azure-kubernetes-service-aks-pulling-private-container-images-from-azure-container-registry-acr-9c3e0a0a13f2
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
