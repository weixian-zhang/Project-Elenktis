using System;
using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message;
using Elenktis.Message.DefaultService;
using Elenktis.MessageBus;
using Elenktis.Policy;
using NServiceBus;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class ASCAutoVMRegisterPolicy : Saga<ASCAutoVMRegisterPolicySagaData>,
        IAmStartedByMessages<AssessASCAutoRegisterVM>,
        IHandleMessages<AssessASCAutoRegisterVMAck>,
        IHandleMessages<FixASCAutoRegisterVMAck>
    {
        public ASCAutoVMRegisterPolicy(IAzure azure, IPlanQueryManager planManager)
        {
            _azure = azure;
            _planManager = planManager;
        }

        protected override void ConfigureHowToFindSaga
            (SagaPropertyMapper<ASCAutoVMRegisterPolicySagaData> mapper)
        {
           mapper.ConfigureMapping<AssessASCAutoRegisterVM>
           (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
           
           mapper.ConfigureMapping<AssessASCAutoRegisterVMAck>
           (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
           
           mapper.ConfigureMapping<FixASCAutoRegisterVMAck>
           (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
        }

        //saga starter
        public async Task Handle(AssessASCAutoRegisterVM message, IMessageHandlerContext context)
        {
            Data.SagaName = "ASCAutoVMRegisterPolicy";
            Data.CorrelationId = message.CorrelationId;
            Data.SubscriptionId = message.SubscriptionId;
            Data.SagaStarterTimeInit = DateTime.Now;

            Data.CreateNewStage("AssessASCAutoRegisterVM");

            await context.Send(QueueDirectory.Spy.DefaultService, message);
        }

        public async Task Handle(AssessASCAutoRegisterVMAck message, IMessageHandlerContext context)
        {
            var stage = Data.Stage("AssessASCAutoRegisterVM");
            stage.TimeReceiveAtHandler = message.TimeCommandReceivedAtHandler;
            stage.TimeAckReceiveAtSaga = DateTime.Now;
            Data.IncurCost = message.IncurCost;
            Data.Policy = message.Policy;
            Data.PolicyValue = message.PolicyValue;
            Data.ToFix = message.ToFix;
            Data.ResourceId = message.AffectedResourceId;
            Data.ResourceType = message.AffectedResourceType;
            
            Data.CreateNewStage("FixASCAutoRegisterVM");

            if(message.ToFix)
            {
                await context.Send
                    (QueueDirectory.Fixer.DefaultService, new FixASCAutoRegisterVM(){
                     CorrelationId = Data.CorrelationId,
                     SubscriptionId = Data.SubscriptionId
                });
            }
            else
            {
                //send to EventLogger
                await context.Send(QueueDirectory.EventLogger.SagaAudit, Data);
                MarkAsComplete();
            }
        }

        public async Task Handle(FixASCAutoRegisterVMAck message, IMessageHandlerContext context)
        {
            var stage = Data.Stage("FixASCAutoRegisterVM");
            stage.ActivityPerformed = message.ActivityPerformed;
            Data.Remediated = message.Remediated;

            await context.Send(QueueDirectory.EventLogger.SagaAudit, Data);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga
            (IConfigureHowToFindSagaWithMessage sagaMessageFindingConfiguration)
        {
            throw new System.NotImplementedException();
        }

        private IAzure _azure;
        private IPlanQueryManager _planManager;
        
    }
} 