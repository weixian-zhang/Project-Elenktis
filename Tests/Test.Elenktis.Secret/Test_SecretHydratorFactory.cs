using System;
using Elenktis.Secret;
using Xunit;

namespace Test.Elenktis.Secret
{
    public class Test_SecretHydratorFactory
    {
        public Test_SecretHydratorFactory()
        {
            
        }

        [Fact]
        public void CreateSecretHydrator_WithEnvVariableDev_ShouldReturnNetCoreSecretHydrator()
        {
           Environment.SetEnvironmentVariable("env", "dev");

           ISecretHydrator hydrator = SecretHydratorFactory.Create();

           Assert.Equal(hydrator.GetType(), typeof(NetCoreSecretHydrator));

           Environment.SetEnvironmentVariable("env", null);
        }

        [Fact]
        public void CreateSecretHydrator_WithEnvVariableProd_ShouldReturnEnvironmentVariableSecretHydrator()
        {
           Environment.SetEnvironmentVariable("env", "prod");

           ISecretHydrator hydrator = SecretHydratorFactory.Create();

           Assert.Equal(hydrator.GetType(), typeof(EnvironmentVariableSecretHydrator));

           Environment.SetEnvironmentVariable("env", null);
        }
    }
}
