using Elenktis.Common.AzResourceManager;
using Elenktis.Common.Configuration;
using Elenktis.Fixer.MandateServiceFixer;
using Microsoft.Azure.Management.OperationalInsights.Models;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Security.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Informer
{
    public class FixerExecutionContext
    {
        public AzMgtSDKCredentials AzSDKCredentials { get; set; }

        public List<TenantSubscription> Subscriptions { get; set; }

        public FixerConfig Config { get; set; }

        public FixerSecret Secrets { get; set; }

        public ResourceGroup ResourceGroup { get; set; }

        public Workspace Workspace { get; set; }

        public WorkspaceSetting WorkspaceSetting { get; set; }

        public AutoProvisioningSetting AutoProvisioningSetting { get; set; }
    }
}
    