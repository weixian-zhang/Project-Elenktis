namespace Elenktis.Configuration
{


    public class ElenktisSecret
    {
        public string CosmosMongoDBConnectionString { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string EtcdUsername { get; set; }

        public string EtcdPassword { get; set; }
    }
}