using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;

namespace Elenktis.Chassis.EventLogger.Event
{
    public class DSSagaEvent : IMessage
    {   
        public virtual string CorrelationId { get; set; }

        public virtual string Controller { get; set; }

        public virtual string SubscriptionId { get; set; }

        public virtual string SagaName { get; set; }

        public virtual string Policy { get; set; }

        public virtual string PolicyValue { get; set; }

        public virtual string ResourceType { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual bool IncurCost { get; set; }

        public virtual bool ToFix { get; set; }

        public virtual bool ToWarn { get; set; } = false; // for policy that cannot fix

        public virtual bool Remediated { get; set; }

        public virtual DateTime SagaStarterTimeInit { get; set; }

        public virtual List<SagaStage> Stages { get; set; } = new List<SagaStage>();

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
            CorrelationId = correlationId;
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
}