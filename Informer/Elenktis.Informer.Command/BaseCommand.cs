using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Informer.Command
{
    public abstract class BaseCommand
    {
        public string SubscriptionId { get; set; }

        public string ResourceGroup { get; set; }
    }
}
