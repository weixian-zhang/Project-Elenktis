using System;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;
using Elenktis.Secret;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Management.Security.Models;
using Newtonsoft.Json;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class SpyService : ISpyService
    {
        public SpyService
            (IEndpointInstance bus, IPlanQueryManager planQueryManager, DSSpySecret secrets)
        {
            _bus = bus;

            _planQueryManager = planQueryManager;
            
            _secrets = secrets;

            _azure = AzureRMFactory.AuthAndCreateInstance
                (_secrets.TenantId,_secrets.ClientId, _secrets.ClientSecret); 
        }

        public async Task ExecuteAsync()
        {
            
            // try{
            //     var subscriptions =
            //             await _azure.SubscriptionManager.GetAllSubscriptionsAsync();

            //         foreach(TenantSubscription sub in subscriptions)
            //         {
            //             _azure.SetSubscriptionId(sub.SubscriptionId);

            //             var dsp =
            //                 await _planQueryManager.GetDefaultServicePlansAsync(sub.SubscriptionId);
                    
            //             await AssessASCAutoRegisterVMEnabled(dsp, sub.SubscriptionId);
            //         }
            // }
            // catch(Exception ex)
            // {
            //     //todo log error to cosmos
            // }
        }
        
        // private async Task AssessASCAutoRegisterVMEnabled
        //     (DefaultServicePlan dsp, string subscriptionId)
        // {
        //     var asc = _azure.SecurityCenterClient;

        //     AutoProvisioningSetting  aps =
        //         await asc.AutoProvisioningSettings.GetAsync("default");

        //     if(aps.AutoProvision == "Off")
        //     {
        //         var comm = new EnableASCAutoRegisterVMCommand()
        //         {
        //             //Action = DefaultServiceFixerAction.EnableASCAutoRegisterVM,
        //             AutoProvision = false,
        //         };
                
        //         //TODO: send command to Saga
        //         await _bus.Send(QueueDirectory.Fixer.DefaultServiceSaga.StartSagaQueue
        //         , JsonConvert.SerializeObject(comm));
        //     }
        //     else
        //     {
        //         //todo log activity to cosmos
        //     }

        // }

        private IEndpointInstance _bus;
        private DSSpySecret _secrets;
        private IAzure _azure;
        private IPlanQueryManager _planQueryManager;
    }
}