using System;
using System.Threading.Tasks;

namespace Elenktis.Log
{
    public interface ILogger
    {
        Task LogError(Exception ex);

        Task LogInfo(string info);
    }
}
