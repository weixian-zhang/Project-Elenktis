using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Message;
using Elenktis.MessageBus;
using MongoDB.Driver;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class AppEventLoggerHandler : IHandleMessages<AppEvent>
    {
        public AppEventLoggerHandler(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task Handle(AppEvent message, IMessageHandlerContext context)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
         
            }
        }
    

        private IMongoDatabase _db;
    }
}