// using System.Threading.Tasks;
// using Elenktis.Message.DefaultService;
// using Elenktis.Policy;
// using NServiceBus;

// namespace Elenktis.Saga.DefaultServiceSaga
// {
//     public class ASCAutoVMRegisterSaga : Saga,
//         IAmStartedByMessages<EnableASCAutoRegisterVMCommand>,
//         IHandleMessages<EnableASCAutoRegisterVMAck>
//     {
//         public ASCAutoVMRegisterSaga(IPlanQueryManager planManager)
//         {
//             //dependency injection IPlanQueryMAnager
//         }

//         protected override void ConfigureHowToFindSaga
//             (IConfigureHowToFindSagaWithMessage sagaMessageFindingConfiguration)
//         {
//             throw new System.NotImplementedException();
//         }

//         public Task Handle(EnableASCAutoRegisterVMCommand message, IMessageHandlerContext context)
//         {
//             throw new System.NotImplementedException();
//         }

//         public Task Handle(EnableASCAutoRegisterVMAck message, IMessageHandlerContext context)
//         {
//             throw new System.NotImplementedException();

//             //prepare audit log
            
//             //send to EventLogger
//         }
//     }
// } 