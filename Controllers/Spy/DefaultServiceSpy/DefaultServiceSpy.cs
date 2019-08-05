using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Configuration;

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
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


using Microsoft.Rest;
using System.Threading;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Elenktis.Assessment;
using TenantSubscription = Elenktis.Azure.TenantSubscription;
using Elenktis.Message.DefaultServices;

namespace Elenktis.Spy
{
    public class DefaultServiceSpy
    {
        public DefaultServiceSpy
            (ISecretHydrator secretHydrator, IAzure azureManager, IPlanManager planManager)
        {
            //_secretHydrator = secretHydrator;
            _azureManager = azureManager;
            _planManager = planManager;
        }

        [FunctionName("DefaultServiceSpy")]
        public async Task Run
            ([TimerTrigger("*/10 * * * * *", RunOnStartup =true, UseMonitor =true)]TimerInfo timerInfo, Microsoft.Extensions.Logging.ILogger log)
        {
            try
            {
               HydrateSecrets();

               InitLogger();

               await AssessDefaultServicesAsync();
            }
            catch (Exception ex)
            {
               _appLogger.Error(ex, "exception thrown at Run()");
               throw;
            }
        }

        private void InitLogger()
        {
            // _activityLogger = new LoggerConfiguration()
            //     .WriteTo.Console()
            //     .CreateLogger();

            // _appLogger = new LoggerConfiguration()
            //     .WriteTo.Console()
            //     .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(Console.Error);

            _appLogger = new LoggerConfiguration()
               .WriteTo.MongoDB(_secrets.CosmosMongoDBConnectionString, collectionName: "AppLog",
               period: TimeSpan.Zero,
               restrictedToMinimumLevel: LogEventLevel.Verbose)
               .WriteTo.Console()
               .CreateLogger();

            _activityLogger = new LoggerConfiguration()
               .WriteTo.MongoDB(_secrets.CosmosMongoDBConnectionString, collectionName: "ActivityLog",
               period: TimeSpan.Zero,
               restrictedToMinimumLevel: LogEventLevel.Verbose)
               .WriteTo.Console()
               .CreateLogger();
        }

        private void HydrateSecrets()
        {
            _secrets = _secretHydrator.Hydrate<ControllerSecret>();
        }

        private async Task AssessDefaultServicesAsync()
        {
            var subscriptions =
                await _azureManager.SubscriptionManager.GetAllSubscriptionsAsync();

            foreach (var sub in subscriptions)
            {
                await CheckASCStandardTier(sub);

                //await CheckASCAutoProvisioningEnabled(sub);

                //await CheckIaaSAntimalwareInstalledOnVM(sub);
            }
        }

        private async Task CheckASCStandardTier(TenantSubscription subscription)
        {
            if(! await ToAssessASCUpgradeStandardTier(subscription.SubscriptionId))
            return;

            ISecurityCenterClient ascClient = new SecurityCenterClient(_azcred);
            ascClient.SubscriptionId = subscription.SubscriptionId;

            var pricings = await ascClient.Pricings.ListAsync();

            
            //prepare command to send
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

        // private async Task CheckASCAutoProvisioningEnabled(TenantSubscription subscription)
        // {
        //     ISecurityCenterClient ascClient = new SecurityCenterClient(_sdkCred);
        //     ascClient.SubscriptionId = subscription.SubscriptionId;

        //     AutoProvisioningSetting  aps = await ascClient.AutoProvisioningSettings.GetAsync("default");

        //     if(aps.AutoProvision == "Off")
        //     {
        //         //send command to fixer
        //     }
        // }

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
        //             //send cmd
        //         }
        //     }
        // }

        private async Task<bool> ToAssessASCUpgradeStandardTier(string subscriptionId)
        {
            var dsplan =
                await _planManager.GetDefaultServicePlansAsync(subscriptionId);

            return dsplan.ASCUpgradeStandardTierPolicy.ToAssess;
        }

        private Logger _appLogger = null;
        private Logger _activityLogger = null;
        private AzSDKCredentials _azcred = null;
        private ISecretHydrator _secretHydrator = null;
        private IAzure _azureManager = null;
        private ControllerSecret _secrets;
        private IPlanManager _planManager;
    }
}
