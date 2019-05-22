using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elenktis.Common.AzResourceManager;
using Elenktis.Common.Configuration;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.OperationalInsights;
using Microsoft.Azure.Management.OperationalInsights.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Management.Security.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;



using Microsoft.Rest;
using RestSharp;

namespace Elenktis.Informer
{
    public static class MandateServiceInformer
    {
        [FunctionName("MandateServiceInformer")]
        public async static Task Run([TimerTrigger("5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            HydrateSecrets();

            _sdkCred = new AzMgtSDKCredentials(_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

            await CheckIfMandatoryServicesExist();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        private static void HydrateSecrets()
        {
            var configInitializer = new FlexVolumeConfigInitializer();
            configInitializer.UsePropertyNameAsSecretName = true;
            _secrets = configInitializer.Initialize<ServiceInformerSecrets>();

            //testing only, will remove
            _secrets.TenantId = "fc418f16-5c93-437d-b743-05e9e2a04d93";
            _secrets.ClientId = "442dcbee-62da-4462-b847-32a8003343f2";
            _secrets.ClientSecret = "A4ATErF:/cbv*-EAr9TdJhMAtpt1Kku2";
        }


        private async static Task CheckIfMandatoryServicesExist()
        {
            var subscriptions =
                new SubscriptionManager(_sdkCred)
                    .GetAllSubscriptions().GetAwaiter().GetResult();

            foreach (var sub in subscriptions)
            {
                
            }
        }

        private async static Task<bool> IsDefaultLAWorkspaceExist(TenantSubscription subscription)
        {
            return true;
        }

        private async static Task<bool> IsASCStandardTier(TenantSubscription subscription)
        {
            return true;
        }

        private async static Task<bool> IsASCAssociatedToDefaultWorkspace(TenantSubscription subscription)
        {
            return true;
        }

        private async static Task<bool> IsASCAutoProvisioningEnabled(TenantSubscription subscription)
        {
            return true;
        }

        private async static Task<bool> IsIaaSAntimalwareInstalledOnVM(TenantSubscription subscription)
        {
            return true;
        }



        private static AzMgtSDKCredentials _sdkCred;

        private static ServiceInformerSecrets _secrets;
    }
}
