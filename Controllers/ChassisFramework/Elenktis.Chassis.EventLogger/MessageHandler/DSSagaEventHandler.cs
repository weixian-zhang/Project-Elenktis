using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Chassis.EventLogger.Event;
using Elenktis.Message;
using Elenktis.MessageBus;
using MongoDB.Driver;
using NServiceBus;

namespace Elenktis.Chassis.EventLogger
{
    public class DSSagaEventHandler : IHandleMessages<DSSagaEvent>
    {
        public DSSagaEventHandler(LogStrategist logStrategist)
        {
            _logStrategist = logStrategist;
        }

        public async Task Handle(DSSagaEvent message, IMessageHandlerContext context)
        {
            try
            {
                await _logStrategist.Log(message.Controller, message);
            }
            catch(Exception ex)
            {
                await context.Send(new ErrorEvent(ex, ControllerUri.EventLogger));
            }
        }

        private LogStrategist _logStrategist;
    }
}