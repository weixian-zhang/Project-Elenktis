using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Chassis.EventLogger.Event;
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
    public class AssessASCAutoEnableVMHandler : IHandleMessages<AssessASCAutoRegisterVM>
    {
        public AssessASCAutoEnableVMHandler(IPlanQueryManager planManager, IAzure azure)
        {
            _planManager = planManager;
            _azure = azure;
        }

        public async Task Handle(AssessASCAutoRegisterVM message, IMessageHandlerContext context)
        {
            try
            {
                DateTime cmdReceivedTime = DateTime.Now;

                _azure.SetCurrentSubscriptionId(message.SubscriptionId);

                var plan = await _planManager.GetDefaultServicePlansAsync(message.SubscriptionId);

                string policy =
                    plan.ASCAutoRegisterVMEnabledPolicy.AsString(message.SubscriptionId, p => p.ToAssess);
                
                string policyValue =
                    plan.ASCAutoRegisterVMEnabledPolicy.AsValueString
                        (plan.ASCAutoRegisterVMEnabledPolicy, (p => p.ToAssess));
               
                var ack = new AssessASCAutoRegisterVMAck();
                ack.SetAcknowledge
                    (message.SubscriptionId, message.CorrelationId, policy, policyValue,
                     "AzureSecurityCenter", "AzureSecurityCenter", true, cmdReceivedTime);

                if(!plan.ASCAutoRegisterVMEnabledPolicy.ToAssess)
                {
                    ack.AddActivity
                        ("Policy ASCAutoRegisterVMEnabledPolicy.ToAssess is set to off, skip assessment");

                    await context.Send(QueueDirectory.Saga.DefaultService, ack);

                    return;
                }
                else
                {
                    var asc = _azure.SecurityCenterClient;

                    AutoProvisioningSetting  aps =
                        await asc.AutoProvisioningSettings.GetAsync("default");

                    if(aps.AutoProvision == "Off")
                    {
                        ack.SetToFix();
                        ack.AddActivity("AutoProvision is Off, set flag to ToFix");
                    }
                    else
                        ack.AddActivity("no action");

                    await context.Send(QueueDirectory.Saga.DefaultService, ack);
                }
            }
            catch(Exception ex)
            {
                await context.Send(QueueDirectory.EventLogger.Error,
                    new ErrorEvent(ex, ControllerUri.DefaultServiceSpy));
            }
        }

        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}