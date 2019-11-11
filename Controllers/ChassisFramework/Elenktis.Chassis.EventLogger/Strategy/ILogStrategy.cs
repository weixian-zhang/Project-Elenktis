using System.Threading.Tasks;

namespace Elenktis.Chassis.EventLogger
{
    public interface ILogStrategy
    {
        Task LogAsync(object data);
    }
}