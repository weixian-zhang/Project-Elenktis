namespace Elenktis.Saga.DefaultServiceSaga
{
    public class SagaSecret
    {
        public string DSSagaSqlConnectionString { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string EtcdHost { get; set; }

        public string EtcdPort { get; set; }

        public string ExecutionFrequencyInMinutes { get; set; }
    }
}