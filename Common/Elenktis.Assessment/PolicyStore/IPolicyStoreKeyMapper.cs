using System.Collections.Generic;
using System.Reflection;

namespace Elenktis.Assessment
{
    public interface IPolicyStoreKeyMapper
    {
        IEnumerable<PolicyKeyMeasureMap> MapPolicytoKeys<T>(T policy) where T : Policy;

        string MapKeyFromMeasureProperty(PropertyInfo measure);
    }
}