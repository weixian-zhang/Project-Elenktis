// using System;
// using System.Threading.Tasks;
// using AutoMapper;
// using Elenktis.Azure;
// using Elenktis.Chassis.EventLogger.Event;
// using Elenktis.Message;
// using Elenktis.Message.DefaultService;
// using Elenktis.MessageBus;
// using Elenktis.Policy;
// using MassTransit;
// using MassTransit.Saga;
// using NServiceBus;

// namespace Elenktis.Saga.DefaultServiceSaga
// {
//     public class ASCAutoVMRegisterPolicy : ISaga,
//         InitiatedBy<AssessPolicy>,
//         Orchestrates<AssessPolicyAck>,
//         Orchestrates<FixPolicyAck>
//     {
//         public Guid CorrelationId { get; set; }
//         public SagaTrackingData SagaTrackingData { get; set; }

//         public ASCAutoVMRegisterPolicy
//             (IAzure azure, IPlanQueryManager planManager, IMapper msgMapper)
//         {
           
//             _azure = azure;
//             _planManager = planManager;
//             _msgMapper = msgMapper;
//         }

//         public Task Consume(ConsumeContext<AssessPolicy> context)
//         {
//             throw new NotImplementedException();
//         }

//         public Task Consume(ConsumeContext<AssessPolicyAck> context)
//         {
//             throw new NotImplementedException();
//         }

//         public Task Consume(ConsumeContext<FixPolicyAck> context)
//         {
//             throw new NotImplementedException();
//         }

//         // protected override void ConfigureHowToFindSaga
//         //     (SagaPropertyMapper<DSSagaTrackingData> mapper)
//         // {
//         //    mapper.ConfigureMapping<AssessASCAutoRegisterVM>
//         //    (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);

//         //    mapper.ConfigureMapping<AssessASCAutoRegisterVMAck>
//         //    (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);

//         //    mapper.ConfigureMapping<FixASCAutoRegisterVMAck>
//         //    (msg => msg.CorrelationId).ToSaga(sagaData => sagaData.CorrelationId);
//         // }

//         //saga starter
//         // public async Task Handle(AssessASCAutoRegisterVM message, IMessageHandlerContext context)
//         // {
//         //     Data.SetSagaOnStart(message.SubscriptionId, ControllerUri.DefaultServiceSaga,
//         //         "ASCAutoVMRegisterPolicy", message.CorrelationId, DateTime.Now);

//         //     Data.CreateNewStage(ControllerUri.DefaultServiceSpy);

//         //     await context.Send(QueueDirectory.Spy.DefaultService, message);
//         // }

//         // public async Task Handle(AssessASCAutoRegisterVMAck message, IMessageHandlerContext context)
//         // {
//         //     try
//         //     {
//         //         var stage = Data.FindStage(ControllerUri.DefaultServiceSpy);
//         //         stage.TimeReceiveAtHandler = message.TimeReceivedAtHandler;
//         //         stage.TimeAckReceiveAtSaga = DateTime.Now;

//         //         Data.SetSagaOnAssessCommandAck
//         //             (message.IncurCost, message.PolicyValue, message.PolicyValue,
//         //             message.AffectedResourceId, message.AffectedResourceType, message.ToFix);

//         //         if(message.ToFix)
//         //         {
//         //             Data.CreateNewStage(ControllerUri.DefaultServiceFixer);

//         //             await IPipelineContextExtensions.Send
//         //                 (context, QueueDirectory.Fixer.DefaultService, new FixASCAutoRegisterVM()
//         //                 {
//         //                     CorrelationId = Data.CorrelationId,
//         //                     SubscriptionId = Data.SubscriptionId
//         //                 });
//         //         }
//         //         else
//         //         {
//         //             var stdEvent =
//         //                 _msgMapper.Map<DSSagaTrackingData, DSSagaEvent>(Data);

//         //             //EventLogger subscribing
//         //             await context.Send(QueueDirectory.EventLogger.DSSagaEvent, stdEvent);

//         //             MarkAsComplete();
//         //         }
//         //     }
//         //     catch(Exception ex)
//         //     {
//         //          //EventLogger subscribing
//         //         await context.Send
//         //             (new ErrorEvent(ex, ControllerUri.DefaultServiceSaga));
//         //     }
//         // }

//         // public async Task Handle(FixASCAutoRegisterVMAck message, IMessageHandlerContext context)
//         // {
//         //     try
//         //     {
//         //         var stage = Data.FindStage(ControllerUri.DefaultServiceFixer);
//         //         stage.ActivityPerformed = message.ActivityPerformed;
//         //         Data.Remediated = message.Remediated;

//         //         var stdEvent =
//         //                 _msgMapper.Map<DSSagaTrackingData, DSSagaEvent>(Data);

//         //         //EventLogger subscribing
//         //         await context.Send(QueueDirectory.EventLogger.DSSagaEvent, stdEvent);

//         //         MarkAsComplete();
//         //     }
//         //     catch(Exception ex)
//         //     {
//         //         //EventLogger subscribing
//         //         await context.Send
//         //             (new ErrorEvent(ex, ControllerUri.DefaultServiceSaga));
//         //     }
//         // }


//         private IAzure _azure;
//         private IPlanQueryManager _planManager;
//         private IMapper _msgMapper;
//     }
// } 