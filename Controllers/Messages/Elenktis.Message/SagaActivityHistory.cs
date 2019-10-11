using System;
using System.Collections.Generic;

namespace Elenktis.Message
{
    public class SagaActivityHistory
    {
        public virtual string ActivityName { get; set; }

        public virtual DateTime ActivityStartTime { get; set; }

        public virtual DateTime ActivityEndTime { get; set; }

        public string Activities { get; set; }
    }
}