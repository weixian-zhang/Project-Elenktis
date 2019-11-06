using System;
using System.Reflection;

namespace Elenktis.Secret
{
    public sealed class SecretHydratorFactory
    {
        public static ISecretHydrator Create()
        {
            string env = Environment.GetEnvironmentVariable("env");
            
            if(env.ToLowerInvariant() == "dev")
            {
                return new NetCoreSecretHydrator();
            }
            else
                return new KubeFlexVolumeSecretHydrator();
        }

        public static ISecretHydrator Create(bool fromEnvironmentVariable)
        {
            if(fromEnvironmentVariable)
                return new EnvironmentVariableSecretHydrator();
            else
                return Create();
        }
    }
}