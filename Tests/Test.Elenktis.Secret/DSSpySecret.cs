namespace Test.Elenktis.Secret
{
    public class DSSpySecret
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string EtcdHost { get; set; }

        public string EtcdPort { get; set; }

        public int ExecutionFrequencyInMinutes { get; set; }
    }
}