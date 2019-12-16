namespace Elenktis.MessageBus
{
    public class QueueDirectory
    {
        public static class Fixer
        {
             public const string DefaultService = "pc.core.fixer.ds";
        }

        public static class EventLogger
        {
            public const string Error = "pc.core.eventlogger.controller.error";

            public const string DefaultServiceWorkflow =
                "pc.core.eventlogger.controller.ds";

        }

        public static class Notifier
        {
            public const string Emaile = "pc.core.notifier.email";

            public const string SMS = "pc.core.notifier.sms";
        }
    }
}