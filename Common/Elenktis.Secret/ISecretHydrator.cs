using System;

namespace Elenktis.Secret
{
    public interface ISecretHydrator
    {
        T Hydrate<T>() where T : class;
    }
}
