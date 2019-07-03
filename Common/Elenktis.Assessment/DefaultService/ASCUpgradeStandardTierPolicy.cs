using System;

namespace Elenktis.Assessment.DefaultService
{
    public class ASCUpgradeStandardTierPolicy : Policy
    {
        public bool Assess { get; set; }
        public bool Remediate { get; set; }
        public bool Ignore { get; set; }
    }
}