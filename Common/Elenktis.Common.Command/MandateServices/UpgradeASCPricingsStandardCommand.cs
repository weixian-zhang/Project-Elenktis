using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Common.Command
{
    public class UpgradeASCPricingsStandardCommand : BaseCommand
    {
        public bool IsVMASCPricingFree { get; set; }

        public bool IsAppServiceASCPricingFree { get; set; }

        public bool IsStorageASCPricingFree { get; set; }

        public bool IsSQLASCPricingFree { get; set; }
    }
}
