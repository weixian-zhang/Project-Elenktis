namespace Elenktis.Chassis.EventLogger
{
    public class EventLoggerSecret
    {
        public string EventLoggerCosmosMongoDBConnectionString { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string MongoUsername { get; set; }

        public string MongoPassword { get; set; }

        public string MongoHost { get; set; }

        public int ExecutionFrequencyInMinutes { get; set; }
    }
}