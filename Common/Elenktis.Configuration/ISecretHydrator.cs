using System;

namespace Elenktis.Configuration
{
    public interface ISecretHydrator
    {
        T Hydrate<T>() where T : class;
    }
}
