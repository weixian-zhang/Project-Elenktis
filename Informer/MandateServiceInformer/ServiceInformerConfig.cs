using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Informer
{
    public class ServiceInformerConfig
    {
        public string CosmosMongoDBUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }
    }
}
