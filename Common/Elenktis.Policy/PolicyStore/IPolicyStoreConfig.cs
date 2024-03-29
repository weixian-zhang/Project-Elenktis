using System;
using System.Linq.Expressions;

namespace Elenktis.Policy
{
    public interface IPolicyStoreConfig
    {
        IPolicyStoreConfig WithSubscription(string subscriptionId);

        IPolicyStoreConfig WithMeasure<TPolicy>
            (Expression<Func<TPolicy, object>> measureExpr) where TPolicy : Policy;
    }
}