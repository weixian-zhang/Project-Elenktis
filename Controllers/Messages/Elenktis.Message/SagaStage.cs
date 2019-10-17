using System;
using System.Collections.Generic;

namespace Elenktis.Message
{
    public class SagaStage
    {
        public virtual string StageName { get; set; }

        public virtual DateTime TimeSentFromSaga { get; set; }

        public virtual DateTime TimeReceiveAtHandler { get; set; }

        public virtual DateTime TimeAckReceiveAtSaga { get; set; }

        public string ActivityPerformed { get; set; }
    }
}