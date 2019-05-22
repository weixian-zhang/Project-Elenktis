using Elenktis.Common.Configuration;
using System;

namespace Elenktis.Fixer.MandateServiceFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static void HydrateConfig()
        {
            var informerConfig = YamlConfigLoader.Load<FixerConfig>();

            
        }
    }
}
