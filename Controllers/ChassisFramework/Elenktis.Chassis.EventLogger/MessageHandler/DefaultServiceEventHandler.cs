using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elenktis.Chassis.EventLogger.Event;
using Elenktis.Message;
using Elenktis.MessageBus;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NServiceBus;

namespace Elenktis.Chassis.EventLogger
{
    public class DefaultServiceEventHandler : IHandleMessages<DSSagaEvent>
    {
        public DefaultServiceEventHandler(LogStrategist logStrategist, ILogger logger)
        {
            _logStrategist = logStrategist;
            _logger = logger;
        }

        public async Task Handle(DSSagaEvent message, IMessageHandlerContext context)
        {
            try
            {
                await _logStrategist.Log(message.Controller, message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //await context.Send(new ErrorEvent(ex, ControllerUri.EventLogger));
            }
        }

        private LogStrategist _logStrategist;
        private ILogger _logger;
    }
}