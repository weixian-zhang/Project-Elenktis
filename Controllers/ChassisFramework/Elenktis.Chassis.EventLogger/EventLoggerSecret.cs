namespace Elenktis.Chassis.EventLogger
{
    public class EventLoggerSecret
    {
        public string EventLoggerCosmosMongoDBConnectionString { get; set; }

        public string ServiceBusConnectionString { get; set; }
    }
}