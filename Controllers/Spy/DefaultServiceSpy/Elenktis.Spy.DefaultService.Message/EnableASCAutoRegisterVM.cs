using System;
using Elenktis.Message;

namespace Elenktis.Spy.DefaultService.Message
{
    public class EnableASCAutoRegisterVM : Command
    {
        public bool AutoProvision { get; set; }
    }
}
