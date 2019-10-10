namespace Elenktis.MessageBus
{
    public class QueueDirectory
    {
        public const string ControllerErrorQueue = "pc.core.error";

        public static class Saga
        {
             public const string DefaultService = "pc.core.saga.ds";

            // public static class Workflow
            // {
            //     public static class DefaultService
            //     {
            //         public const string AssessASCAutoRegisterVMEnabledAck =
            //             "pc.core.spy.ds.ack.assess-asc-auto-register-vm-enabled";

            //         public const string FixASCAutoRegisterVMEnabledAck =
            //             "pc.core.fixer.ds.ack.fix-asc-auto-register-vm-enabled";
            //     }
            // }   
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
            public const string Error = "pc.core.eventlogger.controllererror";

            public const string AppEvent = "pc.core.eventlogger.controllerevent";

            public const string MessageAudit = "pc.core.eventlogger.controllermsgaudit";
        }

        public static class Notifier
        {
            public const string Emaile = "pc.core.notifier.email";

            public const string SMS = "pc.core.notifier.sms";
        }
    }
}