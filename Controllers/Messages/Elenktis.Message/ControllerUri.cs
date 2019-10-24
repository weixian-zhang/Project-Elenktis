using System;
using System.Collections.Generic;
using System.Reflection;
using NServiceBus;

namespace Elenktis.Message
{
    public static class ControllerUri
    {
        public const string DefaultServiceSpy = "spy.defaultservice";
        public const string DefaultServiceFixer = "fixer.defaultservice";
        public const string DefaultServiceSaga = "saga.defaultservice";
        public const string SecurityHygieneSpy = "spy.securityhygiene";
        public const string SecurityHygieneFixer = "fixer.securityhygiene";
        public const string SecurityHygieneSaga = "saga.securityhygiene";
        public const string LogEnableSpy = "spy.logenable";
        public const string LogEnableFixer = "fixer.logenable";
        public const string LogEnableSaga = "saga.logenable";
        public const string BlobAVScanController = "fixer.blobavscan";
    }
}