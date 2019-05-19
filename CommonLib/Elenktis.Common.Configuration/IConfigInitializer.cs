using System;

namespace Elenktis.Common.Configuration
{
    public interface IConfigInitializer
    {
        T Initialize<T>() where T : class;
    }
}
