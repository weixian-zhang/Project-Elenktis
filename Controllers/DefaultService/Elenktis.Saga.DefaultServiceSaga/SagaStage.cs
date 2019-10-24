using System;
using System.Collections.Generic;

namespace Elenktis.Saga.DefaultServiceSaga
{
    public class SagaStage
    {
        public string Controller { get; set; }

        public virtual DateTime TimeSentFromSaga { get; set; }

        public virtual DateTime TimeReceiveAtHandler { get; set; }

        public virtual DateTime TimeAckReceiveAtSaga { get; set; }

        public string ActivityPerformed { get; set; }
    }
}