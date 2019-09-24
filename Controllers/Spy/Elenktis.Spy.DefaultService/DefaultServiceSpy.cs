using System;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Secret;

using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.OperationalInsights;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Management.Security.Models;
using Microsoft.Azure.WebJobs;

using Serilog.Core;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;
using Newtonsoft.Json;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;

namespace Elenktis.Spy
{
    public class DefaultServiceSpy
    {
        public DefaultServiceSpy
            (ISecretHydrator secretHydrator, IAzure azureManager, IPlanQueryManager planManager)
        {
            _azureManager = azureManager;
            _planQueryManager = planManager;
        }

        [FunctionName("DefaultServiceSpy")]
        public async Task Run
            ([TimerTrigger("*/20 * * * * *", RunOnStartup =true, UseMonitor =true)]TimerInfo timerInfo, Microsoft.Extensions.Logging.ILogger log)
        {
            try
            {
               Init();

               await AssessDefaultServicesAsync();
            }
            catch (Exception ex)
            {
               _appLogger.Error(ex, "exception thrown at Run()");
               throw;
            }
        }

        private void Init()
        {
            HydrateSecrets();

        }

        private void InitMsgBus()
        {
            IBusControl _bus = Bus.Factory.CreateUsingAzureServiceBus(config => {
                config.Host(_secrets.ServiceBusConnectionString, c => {});
            }); 
        }

        private void HydrateSecrets()
        {
            var hydrator = SecretHydratorFactory.Create();

            _secrets = hydrator.Hydrate<ControllerSecret>();

            _azcred = new AzSDKCredentials
                (_secrets.TenantId, _secrets.ClientId, _secrets.ClientSecret);
        }

        private async Task AssessDefaultServicesAsync()
        {
            var subscriptions =
                await _azureManager.SubscriptionManager.GetAllSubscriptionsAsync();

            foreach (var sub in subscriptions)
            {
                var dsp =
                    await _planQueryManager.GetDefaultServicePlansAsync(sub.SubscriptionId);

                await CheckASCAutoRegisterVMEnabled(dsp, sub.SubscriptionId);

                //await CheckASCAutoProvisioningEnabled(sub);

                //await CheckIaaSAntimalwareInstalledOnVM(sub);
            }
        }

        #region assess by policies

        private async Task CheckASCAutoRegisterVMEnabled
            (DefaultServicePlan dsp, string subscriptionId)
        {
            ISecurityCenterClient ascClient = new SecurityCenterClient(_azcred);
            ascClient.SubscriptionId = subscriptionId;

            AutoProvisioningSetting  aps =
                await ascClient.AutoProvisioningSettings.GetAsync("default");

            if(aps.AutoProvision == "Off")
            {
                var comm = new EnableASCAutoRegisterVMCommand()
                {
                    //Action = DefaultServiceFixerAction.EnableASCAutoRegisterVM,
                    AutoProvision = false,
                };
                
                //TODO: send command to Saga
                //await _msgSender.SendAsync(_queue, JsonConvert.SerializeObject(comm));
            }
        }

        // private async Task AssociateASCToDefaultLAWorkspace
        //     (DefaultServicePlan dsp, string subscriptionId)
        // {
        //     ISecurityCenterClient asc = new SecurityCenterClient(_azcred);
        //     //asc..GetAsync()
        //     //todo: need to create default workspace and link asc to this workspace
        // }

        private async Task CheckASCIsStandardTier(DefaultServicePlan dsp, string subscriptionId)
        {
            ISecurityCenterClient ascClient = new SecurityCenterClient(_azcred);
            ascClient.SubscriptionId = subscriptionId;

            PricingList pricings = await ascClient.Pricings.ListAsync();

            bool vmASCPricingTier = false;
            bool sqlASCPricingTier = false;
            bool AppServiceASCPricingTier = false;
            bool storageASCPricingTier = false;

            foreach (var p in pricings.Value)
            {
                if (p.Name == "VirtualMachines" && p.PricingTier == "Free")
                    vmASCPricingTier = true;
                else if (p.Name == "SqlServers" && p.PricingTier == "Free")
                    sqlASCPricingTier = true;
                else if (p.Name == "AppServices" && p.PricingTier == "Free")
                    AppServiceASCPricingTier = true;
                else if (p.Name == "StorageAccounts" && p.PricingTier == "Free")
                    storageASCPricingTier = true;
            }

            if(!vmASCPricingTier || !sqlASCPricingTier ||
                !AppServiceASCPricingTier || !storageASCPricingTier)
                {
                    //
                }

            // var ascPricingCmd = new UpgradeASCPricingsStandardCommand();
            // ascPricingCmd.SubscriptionId = subscription.SubscriptionId;

            //send command to upgrade
        }

        // private async Task CheckIaaSAntimalwareInstalledOnVM(TenantSubscription subscription)
        // {
        //     IComputeManagementClient cmc = new ComputeManagementClient(_sdkCred);
        //     cmc.SubscriptionId = subscription.SubscriptionId;

        //     var vms = await cmc.VirtualMachines.ListAllAsync();

        //     foreach(var vm in vms)
        //     {
        //        int iaasAntimalwareCount =
        //             vm.Resources.Count(r => {
        //                     string extName = r.Id.Split('/').Last();
        //                 if (extName == "IaaSAntimalware")
        //                     return true;
        //                 else
        //                     return false;
        //                 });
                
        //         if(iaasAntimalwareCount == 0)
        //         {
        //             //send cmd fixer
        //         }
        //     }
        // }

        #endregion
        private string _queue = "policyctl.fixer.defaultservice";
        private Logger _appLogger = null;
        private Logger _activityLogger = null;
        private AzSDKCredentials _azcred = null;
        private IAzure _azureManager = null;
        private ControllerSecret _secrets;
        private IPlanQueryManager _planQueryManager;
    }
}
