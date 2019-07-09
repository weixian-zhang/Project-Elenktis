using System.Collections.Generic;

namespace Elenktis.Assessment
{
    public interface IPolicyStoreKeyMapper
    {
        IEnumerable<KeyMeasureValue> MapPolicytoConfigKeys<T>(T policy) where T : Policy;
    }
}