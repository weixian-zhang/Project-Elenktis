namespace Elenktis.MessageBus
{
    public class QueueDirectory
    {
        public const string ControllerErrorQueue = "pc.core.error";

        public static class Saga
        {
              public const string DefaultService = "pc.core.saga.ds";
        }

        public static class Spy
        {
            public const string DefaultService = "pc.core.spy.ds";
        }

        public static class Fixer
        {
             public const string DefaultService = "pc.core.fixer.ds";
        }

        public static class EventLogger
        {
            public const string Error = "pc.core.eventlogger.controller.error";

            public const string AppEvent = "pc.core.eventlogger.controller.event";

            public const string SagaAudit = "pc.core.eventlogger.controller.sagaaudit";

            public const string ControllerHealth = "pc.core.eventlogger.controller.health";
        }

        public static class Notifier
        {
            public const string Emaile = "pc.core.notifier.email";

            public const string SMS = "pc.core.notifier.sms";
        }
    }
}