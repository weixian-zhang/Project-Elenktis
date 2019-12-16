using System;
using Automatonymous;
using Elenktis.Message;
using MassTransit.Saga;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class ASCAutoVMRegisterPolicyWorkflow :
        MassTransitStateMachine<ASCAutoVMRegisterPolicyWorkflowData>, ISaga
    {
        public State PolicyAssessStart { get; private set; }
        public State WaitForAssessAck { get; private set; }
        public State FixStarted { get; private set; }
        public State WaitForFixAck { get; private set; }
        public State PolicyAssessComplete { get; private set; }

        public Event<AssessPolicy> AssessPolicyReceived { get;  set; }

        public Event<AssessPolicyAck> AssessPolicyAckReceived { get;  set; }

        public Event<FixPolicy> AssessPolicySent { get;  set; }

        public Guid CorrelationId { get;  set; }

        public ASCAutoVMRegisterPolicyWorkflow()
        {
            //https://lostechies.com/chrispatterson/2009/11/01/building-a-service-gateway-using-masstransit-part-3/

            //https://masstransittemp.readthedocs.io/en/latest/overview/saga.html#defining-a-saga

            Initially(
                When(AssessPolicyReceived)
                    .Then((saga) => 
                    {
                        
                    })
                    .TransitionTo(WaitForAssessAck)
                    
            );

            // During(Initial,
            //     When(CreateReportCommandReceived).Then(context =>
            //     {
            //         context.Instance.CustomerId = context.Data.CustomerId;
            //         context.Instance.RequestTime = context.Data.RequestTime;
            //         context.Instance.ReportId = context.Data.ReportId;
            //     })
            //     .Publish(ctx => new ReportRequestReceivedEvent(ctx.Instance))
            //     .TransitionTo(Submitted)
            //     .ThenAsync(context => Console.Out.WriteLineAsync(context.Instance.ToString()))
        }
    }
}