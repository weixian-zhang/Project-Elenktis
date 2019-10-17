using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;
using Microsoft.Azure.Management.Security;
using Microsoft.Azure.Management.Security.Models;
using Newtonsoft.Json;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class FixASCAutoRegisterVMHandler : IHandleMessages<FixASCAutoRegisterVM>
    {
        public FixASCAutoRegisterVMHandler(IPlanQueryManager planManager, IAzure azure)
        {
            _planManager = planManager;
            _azure = azure;
        }

        public async Task Handle(FixASCAutoRegisterVM message, IMessageHandlerContext context)
        {
            var dsp =
                await _planManager.GetDefaultServicePlansAsync(message.SubscriptionId);

            var ack = new FixASCAutoRegisterVMAck()
            {
                CorrelationId = message.CorrelationId
            };

            if(!dsp.ASCAutoRegisterVMEnabledPolicy.ToRemediate)
            {
                ack.ActivityPerformed = "Policy ASCAutoRegisterVMEnabledPolicy.ToRemediate: is false, no action taken";
                await context.Send(QueueDirectory.Saga.DefaultService, ack);
                return;    
            }

            var asc = _azure.SecurityCenterClient;

            await asc.AutoProvisioningSettings.CreateAsync("pc-default", "On");

            ack.Remediated = true;
            ack.ActivityPerformed = "Policy ASCAutoRegisterVMEnabledPolicy.ToRemediate: AutoProvisioningSettings set to 'On'";
            
            await context.Send(QueueDirectory.Saga.DefaultService, ack);
        }
    

        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}