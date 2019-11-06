using System.Threading.Tasks;
using MongoDB.Driver;

namespace Elenktis.Chassis.EventLogger
 {
    public class DSSagaLogStrategy : ILogStrategy
    {
        public DSSagaLogStrategy(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task Log(object data)
        {
           //var collection = _db.GetCollection<SagaTrackingData>("pccore-ds-saga");

           //await collection.InsertOneAsync(data);
        }

        private IMongoDatabase _db;
    }
}