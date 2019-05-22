using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Informer
{
    public class ServiceInformerSecrets
    {
        public string CosmosMongoDBUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string RabbitMQConnectionString { get; set; }
    }
}
