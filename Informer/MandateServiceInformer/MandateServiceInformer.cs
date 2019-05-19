using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Common.Configuration;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Services.AppAuthentication;
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
        public async static Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {

            ServiceInformerConfig config = HydrateConfiguration();

            var mgtSDKLogin = new AzMgtSDKAuthenticator(config.TenantId, config.ClientId, config.ClientSecret);

            string accessToken =
                AzAuthenticator.GetAccessToken(config.TenantId, config.ClientId, config.ClientSecret);

            var subscriptionIds = GetAllSubscriptionIds(accessToken);

            foreach(string id in subscriptionIds)
            {
                UpgradeSecurityCenterToStandardIfFree(id, mgtSDKLogin);
            }


            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        private static ServiceInformerConfig HydrateConfiguration()
        {
            var configInitializer = new FlexVolumeConfigInitializer();
            configInitializer.UsePropertyNameAsSecretName = true;
            var serviceInformerConfig = configInitializer.Initialize<ServiceInformerConfig>();

            return serviceInformerConfig;
        }

        private static IEnumerable<string> GetAllSubscriptionIds(string accessToken)
        {
            var restClient =
                new RestClient(@"https://management.azure.com/providers/Microsoft.ResourceGraph/resources?api-version=2019-04-01");

            var request = new RestRequest(Method.POST);

            request.AddHeader("Bearer", accessToken);

            request.AddJsonBody(new ResourceGraphInput()
                { Query = "project subscriptionId | distinct subscriptionId" });

            var response = restClient.Execute<List<string>>(request);

            //if(!response.IsSuccessful)
            //serilog to mongo

            var subscriptionIds = response.Data;

            return subscriptionIds;
        }

        private static void UpgradeSecurityCenterToStandardIfFree
            (string subscriptionId, AzMgtSDKAuthenticator sdkAuthenticator)
        {
            ISecurityCenterClient ascClient = new SecurityCenterClient(sdkAuthenticator);
            ascClient.SubscriptionId = subscriptionId;

            var ascPricing = ascClient.Pricings.Get("Free");

            if(ascPricing.PricingTier == "Free")
            {
                var pricing =
                    ascClient.Pricings.UpdateAsync("Standard", "Standard").GetAwaiter().GetResult();
            }

            //https://docs.microsoft.com/en-us/rest/api/securitycenter/
        }

        private static void LogAnalyticsWorkspace()
        {
            
        }
    }
}
