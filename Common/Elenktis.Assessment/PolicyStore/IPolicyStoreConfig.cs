using System;
using System.Linq.Expressions;

namespace Elenktis.Assessment
{
    public interface IPolicyStoreConfig
    {
        IPolicyStoreConfig WithSubscription(string subscriptionId);

        IPolicyStoreConfig WithMeasure<TPolicy>
            (Expression<Func<TPolicy, object>> measureExpr) where TPolicy : Policy;
    }
}