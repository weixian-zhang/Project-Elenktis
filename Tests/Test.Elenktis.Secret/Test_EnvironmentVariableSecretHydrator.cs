using System;
using Elenktis.Secret;
using Xunit;

namespace Test.Elenktis.Secret
{
    public class Test_EnvironmentVariableSecretHydrator : IDisposable
    {
        public Test_EnvironmentVariableSecretHydrator()
        {
            Environment.SetEnvironmentVariable
                ("env", "prod");
            Environment.SetEnvironmentVariable
                ("ClientId", "1550...");
            Environment.SetEnvironmentVariable
                ("ClientSecret", "Amdw...");
            Environment.SetEnvironmentVariable
                ("TenantId", "fc41...");
            Environment.SetEnvironmentVariable
                ("ServiceBusConnectionString", "Endpoint=...");
            Environment.SetEnvironmentVariable
                ("EtcdHost", "http://etcd-client");
            Environment.SetEnvironmentVariable
                ("EtcdPort", "2379");
        }

        [Fact]
        public void Hydrate_WithEnvVariablesSet_SagaSecretIsHydrated()
        {
            var hydrator = SecretHydratorFactory.Create();

            var secrets = hydrator.Hydrate<SagaSecret>();

            Assert.StartsWith("1550", secrets.ClientId);
            Assert.StartsWith("Amdw", secrets.ClientSecret);
            Assert.StartsWith("fc41", secrets.TenantId);
            Assert.StartsWith("Endpoint=", secrets.ServiceBusConnectionString);
            Assert.Equal(@"http://etcd-client", secrets.EtcdHost);
            Assert.Equal("2379", secrets.EtcdPort);
        }

        [Fact]
        public void Hydrate_WithEnvVariablesSet_DSSpySecretIsHydrated()
        {
            var hydrator = SecretHydratorFactory.Create();

            var secrets = hydrator.Hydrate<DSSpySecret>();
            
            Assert.StartsWith("1550", secrets.ClientId);
            Assert.StartsWith("Amdw", secrets.ClientSecret);
            Assert.StartsWith("fc41", secrets.TenantId);
            Assert.StartsWith("Endpoint=", secrets.ServiceBusConnectionString);
            Assert.Equal(@"http://etcd-client", secrets.EtcdHost);
            Assert.Equal("2379", secrets.EtcdPort);
        }

        [Fact]
        public void Hydrate_WithEnvVariablesSet_DSFixerSecretIsHydrated()
        {
            var hydrator = SecretHydratorFactory.Create();

            var secrets = hydrator.Hydrate<DSFixerSecret>();

            Assert.StartsWith("1550", secrets.ClientId);
            Assert.StartsWith("Amdw", secrets.ClientSecret);
            Assert.StartsWith("fc41", secrets.TenantId);
            Assert.StartsWith("Endpoint=", secrets.ServiceBusConnectionString);
            Assert.Equal(@"http://etcd-client", secrets.EtcdHost);
            Assert.Equal("2379", secrets.EtcdPort);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable
                ("env", null);
            Environment.SetEnvironmentVariable
                ("ClientId", null);
            Environment.SetEnvironmentVariable
                ("ClientSecret", null);
            Environment.SetEnvironmentVariable
                ("TenantId", null);
            Environment.SetEnvironmentVariable
                ("ServiceBusConnectionString", null);
            Environment.SetEnvironmentVariable
                ("EtcdHost", null);
            Environment.SetEnvironmentVariable
                ("EtcdPort", null);
        }
    }
}