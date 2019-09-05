using System;

namespace Elenktis.Secret
{
    public sealed class SecretHydratorFactory
    {
        public static ISecretHydrator Create()
        {
            string env = Environment.GetEnvironmentVariable("env");

            if(env.ToLowerInvariant() == "dev")
                return new NetCoreSecretHydrator();
            else
                return new AKVSecretHydrator();
        }
    }
}