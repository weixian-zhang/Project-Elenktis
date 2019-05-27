using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elenktis.Common.AzResourceManager;
using Elenktis.Common.Configuration;
using Elenktis.Common.Command;
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

namespace Elenktis.Informer.MandateServiceInformer
{
    public static class MandateServiceInformer
    {
        [FunctionName("MandateServiceInformer")]
        public async static Task Run([TimerTrigger("5 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            HydrateSecrets();

            _sdkCred = new AzMgtSDKCredentials(_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);

            _config = YamlConfigLoader.Load<InformerConfig>();

            await CheckIfMandatoryServicesExist();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }

        private static void HydrateSecrets()
        {
            var configInitializer = new FlexVolumeConfigInitializer();
            configInitializer.UsePropertyNameAsSecretName = true;
            _secrets = configInitializer.Initialize<ServiceInformerSecrets>();

            //testing only, will remove
            //_secrets = new ServiceInformerSecrets();
            _secrets.TenantId = "fc418f16-5c93-437d-b743-05e9e2a04d93";
            _secrets.ClientId = "442dcbee-62da-4462-b847-32a8003343f2";
            _secrets.ClientSecret = "A4ATErF:/cbv*-EAr9TdJhMAtpt1Kku2";
        }

        private async static Task CheckIfMandatoryServicesExist()
        {
            var subscriptions = await new SubscriptionManager(_sdkCred).GetAllSubscriptionsAsync();

            foreach (var sub in subscriptions)
            {
                await CheckASCStandardTier(sub);

                await CheckASCAutoProvisioningEnabled(sub);

                await CheckIaaSAntimalwareInstalledOnVM(sub);
            }
        }

        private async static Task CheckASCStandardTier(TenantSubscription subscription)
        {
            ISecurityCenterClient ascClient = new SecurityCenterClient(_sdkCred);
            ascClient.SubscriptionId = subscription.SubscriptionId;

            var pricings = await ascClient.Pricings.ListAsync();

            var ascPricingCmd = new UpgradeASCPricingsStandardCommand();
            ascPricingCmd.SubscriptionId = subscription.SubscriptionId;

            foreach (var p in pricings.Value)
            {
                if (p.Name == "VirtualMachines" && p.PricingTier == "Free")
                    ascPricingCmd.IsVMASCPricingFree = true;
                else if (p.Name == "SqlServers" && p.PricingTier == "Free")
                    ascPricingCmd.IsSQLASCPricingFree = true;
                else if (p.Name == "AppServices" && p.PricingTier == "Free")
                    ascPricingCmd.IsAppServiceASCPricingFree = true;
                else if (p.Name == "StorageAccounts" && p.PricingTier == "Free")
                    ascPricingCmd.IsStorageASCPricingFree = true;
            }

            //send command to upgrade
        }

        private async static Task CheckASCAutoProvisioningEnabled(TenantSubscription subscription)
        {
            ISecurityCenterClient ascClient = new SecurityCenterClient(_sdkCred);
            ascClient.SubscriptionId = subscription.SubscriptionId;

            AutoProvisioningSetting  aps = await ascClient.AutoProvisioningSettings.GetAsync("default");

            if(aps.AutoProvision == "Off")
            {
                //send command to fixer
            }
        }

        private async static Task CheckIaaSAntimalwareInstalledOnVM(TenantSubscription subscription)
        {
            IComputeManagementClient cmc = new ComputeManagementClient(_sdkCred);
            cmc.SubscriptionId = subscription.SubscriptionId;

            var vms = await cmc.VirtualMachines.ListAllAsync();

            foreach(var vm in vms)
            {
               int iaasAntimalwareCount =
                    vm.Resources.Count(r => {
                            string extName = r.Id.Split('/').Last();
                        if (extName == "IaaSAntimalware")
                            return true;
                        else
                            return false;
                        });
                
                if(iaasAntimalwareCount == 0)
                {
                    //send cmd
                }
            }
        }

        private static AzMgtSDKCredentials _sdkCred;

        private static ServiceInformerSecrets _secrets;

        private static InformerConfig _config;
    }
}
