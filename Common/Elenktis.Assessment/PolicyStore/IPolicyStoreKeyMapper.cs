﻿using System.Collections.Generic;
using System.Reflection;

namespace Elenktis.Assessment
{
    public interface IPolicyStoreKeyMapper
    {
        IEnumerable<PolicyKeyMeasureMap> GetKeyMeasureMap<T>
            (string subscriptionId, T policy) where T : Policy;

        string CreatePolicyStoreKey
            (string subscriptionId, string assessmentPlanName, string policyName, string measureName);

        //string MapKeyFromMeasureProperty(PropertyInfo measure);
    }
}