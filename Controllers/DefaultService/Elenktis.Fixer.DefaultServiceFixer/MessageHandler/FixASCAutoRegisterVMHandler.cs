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
            try{
                _azure.SetCurrentSubscriptionId(message.SubscriptionId);
                
                var dsp =
                    await _planManager.GetDefaultServicePlansAsync(message.SubscriptionId);

                var ack = new FixASCAutoRegisterVMAck();
                ack.SetAcknowledge
                    (message.SubscriptionId, message.CorrelationId, DateTime.Now);

                if(!dsp.ASCAutoRegisterVMEnabledPolicy.ToRemediate)
                {
                    ack.AddActivity
                        ("Policy ASCAutoRegisterVMEnabledPolicy.ToRemediate: is false, no action taken");
                    await context.Send(QueueDirectory.Saga.DefaultService, ack);
                    return;    
                }

                var asc = _azure.SecurityCenterClient;

                await asc.AutoProvisioningSettings.CreateAsync("default", "On");

                ack.Remediated = true;
                ack.AddActivity
                    ("Policy ASCAutoRegisterVMEnabledPolicy.ToRemediate: AutoProvisioningSettings set to 'On'");
                
                var options = new ReplyOptions();
                options.RouteReplyTo(QueueDirectory.Saga.DefaultService);
                await context.Reply(ack, options);
            }
            catch(Exception ex)
            {
                await context.Send(new ErrorEvent(ex, ControllerUri.DefaultServiceFixer));
            }
        }
    

        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}