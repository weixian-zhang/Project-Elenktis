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
        IConsumer<ASCAutoRegisterVMPolicyFix>,
        IPolicyFixer<ASCAutoRegisterVMPolicyFix>
    {
        public ASCAutoRegisterVMPolicyFixer
            (DSFixerSecret secret, IPlanQueryManager planManager, IAzure azure)
        {
            _secret = secret;
            _planManager = planManager;
            _azure = azure;
        }

        public async Task Consume(ConsumeContext<ASCAutoRegisterVMPolicyFix> context)
        {
            try
            {
                ASCAutoRegisterVMPolicyFix policyFixMsg = context.Message;

                SetCurrentAzureSubscriptionId(context.Message.SubscriptionId, _azure);

                policyFixMsg =
                    await AssessPolicy(policyFixMsg, _azure, _planManager);

                policyFixMsg =
                    await FixPolicy(policyFixMsg, _azure, _planManager);

                var uriBuilder = new UriBuilder
                    ("https", _secret.ASBHost, 443, QueueDirectory.EventLogger.DefaultServiceWorkflow);
                
                var sendEndpoint =
                    await context.GetSendEndpoint(uriBuilder.Uri);
                
                await sendEndpoint.Send(policyFixMsg);
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

        public async Task<ASCAutoRegisterVMPolicyFix> AssessPolicy
            (ASCAutoRegisterVMPolicyFix policyFixMsg, IAzure azureManager, IPlanQueryManager policyQueryManager)
        {
            policyFixMsg.TimeReceivedAtFixer = DateTime.Now;
            policyFixMsg.SubscriptionId = policyFixMsg.SubscriptionId;
            policyFixMsg.CorrelationId = policyFixMsg.CorrelationId;
            policyFixMsg.TimeSentToFixerFromTriggerer = DateTime.Now;
            
            var dsp =
                await policyQueryManager.
                    GetDefaultServicePlansAsync(policyFixMsg.SubscriptionId);
            
            policyFixMsg.AssessPolicyName =
                dsp.ASCAutoRegisterVMEnabledPolicy.AsString
                (policyFixMsg.SubscriptionId, p => p.ToAssess);
            
            policyFixMsg.ToAssess = dsp.ASCAutoRegisterVMEnabledPolicy.ToAssess;

            if(!policyFixMsg.ToAssess)
                return policyFixMsg;

            var settings = await azureManager.SecurityCenterClient
                .AutoProvisioningSettings.ListAsync();
            
            if(!settings.Any(s => s.Name == "default" && s.AutoProvision == "On"))
            {
                policyFixMsg.AddActivity($"Assessment: AutoProvision settings not found");
                policyFixMsg.PostAssessToFix = true;
            }
            else
            {
                policyFixMsg.PostAssessToFix = false;
                policyFixMsg.AddActivity("AutoProvision settings already exist");
            }
        
            return policyFixMsg;
        }

        public async Task<ASCAutoRegisterVMPolicyFix> FixPolicy
            (ASCAutoRegisterVMPolicyFix policyFixMsg, IAzure azureManager, IPlanQueryManager policyQueryManager)
        {
           var dsp = await policyQueryManager.GetDefaultServicePlansAsync(policyFixMsg.SubscriptionId);
        
            policyFixMsg.FixPolicyName =
                dsp.ASCAutoRegisterVMEnabledPolicy.AsString
                    (policyFixMsg.SubscriptionId, p=>p.ToAssess);

            policyFixMsg.ToFix = dsp.ASCAutoRegisterVMEnabledPolicy.ToRemediate;

            if(!policyFixMsg.PostAssessToFix)
            {
                 policyFixMsg.AddActivity("Fixer skipped: Either AutoProvisioningSettings exists or ToFix Policy = false");
            }
            else
            {
                if(policyFixMsg.ToFix)
                {
                    await _azure.SecurityCenterClient.AutoProvisioningSettings.CreateAsync("default", "On");
        
                    policyFixMsg.AddActivity("Fixer: AutoProvisioningSettings created with Name 'default' and value 'On'");
                }
            }

            return policyFixMsg;
        }


        private DSFixerSecret _secret;
        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}