using System;
using NServiceBus;

namespace Elenktis.Message
{
    public class ASCAutoVMRegisterPolicySagaData : SagaTrackingData
    {
        public DateTime SagaStarterTimeInit { get; set; }

        public DateTime AssessASCVMAutoRegTimeSentFromSaga { get; set; }

        public DateTime AssessASCVMAutoRegTimeReceiveAtHandler { get; set; }

        public DateTime AssessASCVMAutoRegAckTimeSentFromHandler { get; set; }

        public DateTime AssessASCVMAutoRegAckTimeReceiveAtSaga { get; set; } = DateTime.MinValue;

        public DateTime FixASCAutoRegisterVMTimeSentFromSaga { get; set; } = DateTime.MinValue;

        public DateTime FixASCAutoRegisterVMTimeReceivedAtHandler { get; set; } = DateTime.MinValue;

        public DateTime FixASCAutoRegisterVMAckTimeSentFromHandler  { get; set; } = DateTime.MinValue;

        public DateTime FixASCAutoRegisterVMAckTimeReceiveAtSaga  { get; set; } = DateTime.MinValue;
    }
}