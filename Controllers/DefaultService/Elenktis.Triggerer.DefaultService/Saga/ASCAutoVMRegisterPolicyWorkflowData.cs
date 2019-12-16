using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Automatonymous;
using Elenktis.Message;
using MassTransit.Saga;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class ASCAutoVMRegisterPolicyWorkflowData : SagaStateMachineInstance
    {  
        public State CurrentState { get; set; }
        
        public string Controller { get; set; }

        public string SubscriptionId { get; set; }

        public string SagaName { get; set; }

        public string Policy { get; set; }

        public string PolicyValue { get; set; }

        public string ResourceType { get; set; }

        public string ResourceId { get; set; }

        public bool IncurCost { get; set; }

        public bool ToFix { get; set; }

        public bool ToWarn { get; set; } = false; // for policy that cannot fix

        public bool Remediated { get; set; }

        public DateTime SagaStarterTimeInit { get; set; }

        public List<SagaStage> Stages { get; set; } = new List<SagaStage>();

        public Guid CorrelationId { get; set; }

        public SagaStage CreateNewStage(string controller)
        {
            var newStage = new SagaStage(){
                Controller = controller,
                TimeSentFromSaga = DateTime.Now
            };

            Stages.Add(newStage);

            return newStage;
        }

        public SagaStage FindStage(string controller)
        {
            return Stages.Single(s => s.Controller == controller);
        }

        public void SetSagaOnStart
            (string subscriptionId, string controller,
             string sagaName, string correlationId, DateTime sagaStarterTimeInit)
        {
            SubscriptionId = subscriptionId;
            Controller = controller;
            SagaName = sagaName;
            CorrelationId = Guid.NewGuid();
            SagaStarterTimeInit = sagaStarterTimeInit;
        }

        public void SetSagaOnAssessCommandAck
            (bool incurCost, string policy, string policyValue, string affectedResourceId,
             string affectedResourceType, bool toFix)
        {
            IncurCost = IncurCost;
            Policy = policy;
            PolicyValue = policyValue;
            ResourceId = affectedResourceId;
            ResourceType = affectedResourceType;
            ToFix = toFix;
        }

        public void SetSagaOnFixCommandAck()
        {

        }
    }

    public class SagaStage
    {
        public string Controller { get; set; }

        public virtual DateTime TimeSentFromSaga { get; set; }

        public virtual DateTime TimeReceiveAtHandler { get; set; }

        public virtual DateTime TimeAckReceiveAtSaga { get; set; }

        public string ActivityPerformed { get; set; }
    }
}