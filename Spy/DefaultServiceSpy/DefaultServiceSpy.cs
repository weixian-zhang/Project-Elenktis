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

namespace Elenktis.Spy
{
    public class DefaultServiceSpy
    {
        public DefaultServiceSpy(ISecretHydrator secretHydrator, IAzure azureManager)
        {
            _secretHydrator = secretHydrator;
            _azureManager = azureManager;
        }

        

        [FunctionName("DefaultServiceSpy")]
        public async Task Run
            ([TimerTrigger("*/10 * * * * *", RunOnStartup =true, UseMonitor =true)]TimerInfo timerInfo, Microsoft.Extensions.Logging.ILogger log)
        {
            try
            {
               //HydrateSecrets();

               //InitLogger();
            //string etcdUrl = Environment.GetEnvironmentVariable("etcdUrl");

            //    var configStore =
            //         new EtcdPolicyStore("etcd-client", 2379);


               //await CheckIfMandatoryServicesExist();

               log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
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
            _secrets = _secretHydrator.Hydrate<ElenktisSecret>();

            //testing only, will remove
            //_secrets = new ServiceInformerSecrets();
            //_secrets.TenantId = "fc418f16-5c93-437d-b743-05e9e2a04d93";
            //_secrets.ClientId = "442dcbee-62da-4462-b847-32a8003343f2";
            //  _secrets.ClientSecret = "A4ATErF:/cbv*-EAr9TdJhMAtpt1Kku2";
        }

        // private async Task CheckIfMandatoryServicesExist()
        // {
        //     var subscriptions = await new SubscriptionManager(_sdkCred).GetAllSubscriptionsAsync();

        //     foreach (var sub in subscriptions)
        //     {
        //         await CheckASCStandardTier(sub);

        //         await CheckASCAutoProvisioningEnabled(sub);

        //         await CheckIaaSAntimalwareInstalledOnVM(sub);
        //     }
        // }

        // private async Task CheckASCStandardTier(TenantSubscription subscription)
        // {
        //     ISecurityCenterClient ascClient = new SecurityCenterClient(_sdkCred);
        //     ascClient.SubscriptionId = subscription.SubscriptionId;

        //     var pricings = await ascClient.Pricings.ListAsync();

        //     var ascPricingCmd = new UpgradeASCPricingsStandardCommand();
        //     ascPricingCmd.SubscriptionId = subscription.SubscriptionId;

        //     foreach (var p in pricings.Value)
        //     {
        //         if (p.Name == "VirtualMachines" && p.PricingTier == "Free")
        //             ascPricingCmd.IsVMASCPricingFree = true;
        //         else if (p.Name == "SqlServers" && p.PricingTier == "Free")
        //             ascPricingCmd.IsSQLASCPricingFree = true;
        //         else if (p.Name == "AppServices" && p.PricingTier == "Free")
        //             ascPricingCmd.IsAppServiceASCPricingFree = true;
        //         else if (p.Name == "StorageAccounts" && p.PricingTier == "Free")
        //             ascPricingCmd.IsStorageASCPricingFree = true;
        //     }

        //     //send command to upgrade
        // }

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

        private Logger _appLogger = null;
        private Logger _activityLogger = null;
        private ISecretHydrator _secretHydrator = null;
        private IAzure _azureManager = null;
        private static ElenktisSecret _secrets;
    }
}
