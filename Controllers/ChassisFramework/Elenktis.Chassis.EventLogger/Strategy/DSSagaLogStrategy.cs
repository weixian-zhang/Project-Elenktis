using System.Threading.Tasks;
using Elenktis.Chassis.EventLogger.Event;
using MongoDB.Driver;

namespace Elenktis.Chassis.EventLogger
 {
    public class DSSagaLogStrategy : ILogStrategy
    {
        public DSSagaLogStrategy(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task LogAsync(object data)
        {
           var collection = _db.GetCollection<DSSagaEvent>(_collection);

           await collection.InsertOneAsync((DSSagaEvent)data);
        }

        private const string _collection = "pccore-ds-saga";

        private IMongoDatabase _db;
    }
}