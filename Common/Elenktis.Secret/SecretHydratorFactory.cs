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
                // if(assembly == null)
                //     throw new ArgumentException
                //     ("Assembly parameter with ASP.Net Core UserSecret attribute cannot be null");

                return new NetCoreSecretHydrator();
            }
            else
                return new KubeFlexVolumeSecretHydrator();
        }

        // public static ISecretHydrator Create(string env = "dev", Assembly assembly = null)
        // {
        //     if(env.ToLowerInvariant() == "dev")
        //     {
        //         if(assembly == null)
        //             throw new ArgumentException
        //             ("Assembly parameter with ASP.Net Core UserSecret attribute cannot be null");

        //         return new NetCoreSecretHydrator();
        //     }
        //     else
        //         return new KubeFlexVolumeSecretHydrator();
        // }
    }
}