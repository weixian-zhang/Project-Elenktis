using System;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Chassis.EventLogger.Event;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using Elenktis.Policy;
using MassTransit;
using Microsoft.Azure.Management.Security;
using NServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace Elenktis.Fixer.DefaultService
{
    public class ASCAutoRegisterVMPolicyFixer :
        IConsumer<ASCAutoRegisterVMPolicyStart>,
        IPolicyFixer<ASCAutoRegisterVMPolicyStart, ASCAutoRegisterVMPolicyComplete>
    {
        public ASCAutoRegisterVMPolicyFixer
            (DSFixerSecret secret, IPlanQueryManager planManager, IAzure azure)
        {
            _secret = secret;
            _planManager = planManager;
            _azure = azure;
        }

        public async Task Consume(ConsumeContext<ASCAutoRegisterVMPolicyStart> context)
        {
            try
            {
                SetCurrentAzureSubscriptionId(context.Message.SubscriptionId, _azure);

                var policyCompleteMsg = await AssessPolicy(context.Message, _azure, _planManager);

                await FixPolicy(policyCompleteMsg, _azure, _planManager);

                policyCompleteMsg.TimeSentFromFixer = DateTime.Now;

                var uriBuilder = new UriBuilder
                    ("https", _secret.ASBHost, 443, QueueDirectory.EventLogger.DefaultServiceWorkflow);
                
                var sendEndpoint =
                    await context.GetSendEndpoint(uriBuilder.Uri);
                
                await sendEndpoint.Send(policyCompleteMsg);
            }
            catch(Exception ex)
            {
                //TODO: serilog send elasticsearch
                throw ex;
            }
        }

        public void SetCurrentAzureSubscriptionId(string subscriptionId, IAzure azureManager)
        {
            _azure.SetCurrentSubscriptionId(subscriptionId);
        }

        public async Task<ASCAutoRegisterVMPolicyComplete> AssessPolicy
            (ASCAutoRegisterVMPolicyStart policyStartMsg, IAzure azureManager, IPlanQueryManager policyQueryManager)
        {
            var policyCompleteMsg = new ASCAutoRegisterVMPolicyComplete();
            policyCompleteMsg.SubscriptionId = policyStartMsg.SubscriptionId;
            policyCompleteMsg.CorrelationId = policyStartMsg.CorrelationId;
            policyCompleteMsg.TimeReceivedFromTriggerer = DateTime.Now;
            
            var dsp =
                await policyQueryManager.GetDefaultServicePlansAsync(policyCompleteMsg.SubscriptionId);
            
            policyCompleteMsg.AssessPolicyName =
                dsp.ASCAutoRegisterVMEnabledPolicy.AsString
                (policyCompleteMsg.SubscriptionId, p => p.ToAssess);
            
            policyCompleteMsg.ToAssess = dsp.ASCAutoRegisterVMEnabledPolicy.ToAssess;

            if(!policyCompleteMsg.ToAssess)
                return policyCompleteMsg;

            var settings = await azureManager.SecurityCenterClient
                .AutoProvisioningSettings.ListAsync();
            
            if(!settings.Any(s => s.Name == "default" && s.AutoProvision == "On"))
                policyCompleteMsg.AddActivity($"Assessment: AutoProvision settings not found");
            else
            {
                policyCompleteMsg.IsResourceSettingExist = true;
                policyCompleteMsg.AddActivity($"Assessment Skipped: {policyCompleteMsg.AssessPolicyName} = {policyCompleteMsg.ToAssess.ToString()}");
            }
        
            return policyCompleteMsg;
        }

        public async Task FixPolicy
            (ASCAutoRegisterVMPolicyComplete policyCompleteMsg, IAzure azureManager, IPlanQueryManager policyQueryManager)
        {
           var dsp = await policyQueryManager.GetDefaultServicePlansAsync(policyCompleteMsg.SubscriptionId);
        
            policyCompleteMsg.FixPolicyName =
                dsp.ASCAutoRegisterVMEnabledPolicy.AsString
                    (policyCompleteMsg.SubscriptionId, p=>p.ToAssess);

            policyCompleteMsg.ToFix = dsp.ASCAutoRegisterVMEnabledPolicy.ToRemediate;

            if(policyCompleteMsg.ToFix && !policyCompleteMsg.IsResourceSettingExist)
            {
                await _azure.SecurityCenterClient.AutoProvisioningSettings.CreateAsync("default", "On");
            
                policyCompleteMsg.AddActivity("Fixer: AutoProvisioningSettings created with Name 'default' and value 'On'");
            }
            else
                policyCompleteMsg.AddActivity("Fixer skipped: Either AutoProvisioningSettings exists or ToFix Policy = false");
        }


        private DSFixerSecret _secret;
        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}