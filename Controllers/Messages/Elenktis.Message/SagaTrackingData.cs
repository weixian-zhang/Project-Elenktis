using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NServiceBus;

namespace Elenktis.Message
{
    public abstract class SagaTrackingData : ContainSagaData
    {
        public virtual string CorrelationId { get; set;}
        
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

        public virtual List<SagaStage> Stages { get; set; } = new List<SagaStage>();

        public SagaStage CreateNewStage(string stageName)
        {
            var newStage = new SagaStage(){
                StageName = stageName,
                TimeSentFromSaga = DateTime.Now
            };

            Stages.Add(newStage);

            return newStage;
        }

        public SagaStage Stage(string stageName)
        {
            return Stages.Single(s => s.StageName == stageName);
        }

        //public DateTime SagaStarterTimeInit { get; set; }

        // public DateTime AssessActivityTimeSentFromSaga { get; set; }

        // public DateTime AssessActivityTimeReceiveAtHandler { get; set; }

        // public DateTime AssessActivityAckTimeSentFromHandler { get; set; }

        // public DateTime AssessActivityAckTimeReceiveAtSaga { get; set; } = DateTime.MinValue;

        // public DateTime FixASCAutoRegisterVMTimeSentFromSaga { get; set; } = DateTime.MinValue;

        // public DateTime FixASCAutoRegisterVMTimeReceivedAtHandler { get; set; } = DateTime.MinValue;

        // public DateTime FixASCAutoRegisterVMAckTimeSentFromHandler  { get; set; } = DateTime.MinValue;

        // public DateTime FixASCAutoRegisterVMAckTimeReceiveAtSaga  { get; set; } = DateTime.MinValue;
    }
}