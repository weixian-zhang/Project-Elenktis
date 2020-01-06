using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Chassis.EventLogger.Event;
using Elenktis.Message;
using MongoDB.Driver;

namespace Elenktis.Chassis.EventLogger
{
    public class LogStrategist
    {
        public LogStrategist(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task Log(string controllerName, object data)
        {
            switch(controllerName)
            {
                case ControllerUri.DefaultServiceSaga:
                    await new DefaultServiceWorkflowTrackLogStrategy(_db).LogAsync(data);
                    break;
            }
        }

        private IMongoDatabase _db;
    }
}