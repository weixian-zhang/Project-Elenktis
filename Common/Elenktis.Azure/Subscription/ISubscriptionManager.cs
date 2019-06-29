using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elenktis.Azure
{
    public interface ISubscriptionManager
    {
        Task<IEnumerable<TenantSubscription>> GetAllSubscriptionsAsync();
    }
}