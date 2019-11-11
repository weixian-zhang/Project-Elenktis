using System;
using System.Threading.Tasks;
using Elenktis.Chassis.EventLogger;
using Elenktis.Chassis.EventLogger.Event;
using MongoDB.Driver;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class ErrorEventHandler : IHandleMessages<ErrorEvent>
    {
        public ErrorEventHandler(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task Handle(ErrorEvent message, IMessageHandlerContext context)
        {
            try
            {
                var errorCollection =  _db.GetCollection<ErrorEvent>(_collection);

                await errorCollection.InsertOneAsync(message);
            }
            catch(Exception ex)
            {
         
            }
        }

        private const string _collection = "pccore-error";

        private IMongoDatabase _db;
    }
}