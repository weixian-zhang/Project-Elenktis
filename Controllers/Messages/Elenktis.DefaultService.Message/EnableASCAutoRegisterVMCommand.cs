using System;
using Elenktis.Message;

namespace Elenktis.Message.DefaultService
{
    public class EnableASCAutoRegisterVMCommand : Command
    {
        public bool AutoProvision { get; set; }
    }
}
    