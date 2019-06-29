using Microsoft.Azure.Management.ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Azure
{
    public class TenantSubscription
    {
        public TenantSubscription()
        {
            ResourceGroups = new List<ResourceGroup>();
        }

        public string SubscriptionId { get; set; }

        public string DisplayName { get; set; }

        public bool IsResourceGroupExist { get; set; }

        public IEnumerable<ResourceGroup> ResourceGroups { get; set; }
    }
}
