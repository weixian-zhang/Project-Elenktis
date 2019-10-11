using System;
using System.Collections.Generic;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public static class ControllerUri
    {
        public const string DefaultServiceSpy = "controllers.spy.defaultservice";
        public const string DefaultServiceFixer = "controllers.fixer.defaultservice";
        public const string SecurityHygieneSpy = "controllers.spy.securityhygiene";
        public const string SecurityHygieneFixer = "controllers.fixer.securityhygiene";
        public const string LogEnableSpy = "controllers.spy.logenable";
        public const string LogEnableFixer = "controllers.fixer.logenable";
        public const string BlobAVScanController = "controllers.fixer.blobavscan";
    }
}