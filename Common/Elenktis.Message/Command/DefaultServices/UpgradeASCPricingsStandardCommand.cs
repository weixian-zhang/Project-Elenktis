using System;
using System.Collections.Generic;
using System.Text;
using Elenktis.Command;

namespace Elenktis.Message.DefaultServices
{
    public class UpgradeASCPricingsStandardCommand : BaseCommand
    {
        public bool IsVMASCPricingFree { get; set; }

        public bool IsAppServiceASCPricingFree { get; set; }

        public bool IsStorageASCPricingFree { get; set; }

        public bool IsSQLASCPricingFree { get; set; }
    }
}
