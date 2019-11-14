using System;
using System.Reflection;

namespace Elenktis.Secret
{
    public sealed class SecretHydratorFactory
    {
        public static ISecretHydrator Create()
        {
            string env = Environment.GetEnvironmentVariable("env");
            
            if(string.IsNullOrEmpty(env) || env.ToLowerInvariant() == "dev")
                return new NetCoreSecretHydrator();
            else
                return new EnvironmentVariableSecretHydrator();
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