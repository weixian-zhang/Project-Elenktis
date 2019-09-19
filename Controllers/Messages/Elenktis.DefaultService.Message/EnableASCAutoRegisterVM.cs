using System;
using Elenktis.Message;

namespace Elenktis.DefaultService.Message
{
    public class EnableASCAutoRegisterVM : Command
    {
        public bool AutoProvision { get; set; }
    }
}
    