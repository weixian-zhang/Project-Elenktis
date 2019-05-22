using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Fixer.MandateServiceFixer
{
    public class FixerSecret
    {
        public string CosmosMongoDBUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string RabbitMQConnectionString { get; set; }
    }
}
