namespace Elenktis.MessageBus
{
    public static class ControllerUri
    {
        public const string DefaultServiceTriggerer = "triggerer.defaultservice";
        public const string DefaultServiceFixer = "fixer.defaultservice";
        public const string SecurityHygieneTriggerer = "triggerer.securityhygiene";
        public const string SecurityHygieneFixer = "fixer.securityhygiene";
        public const string MonitorTriggerer = "triggerer.monitor";
        public const string MonitorFixer = "fixer.monitor";
        public const string BlobAVScan = "fixer.blobavscan";
        public const string EventLogger = "chassis.eventlogger";
        public const string Notifier = "chassis.notifier";
        public const string AIASAssessor = "assessor.aias";
    }
}