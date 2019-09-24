using System;
using System.Threading;
using Elenktis.Azure;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;
using Elenktis.Secret;

namespace Elenktis.Fixers.DefaultServiceFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://github.com/MassTransit/Sample-DotNetCore-Request/blob/master/Sample-RequestResponse/Program.cs

            _autoResetEvent.WaitOne();
        }

        private void InitMsgHandlers()
        {
            // var endpointConfiguration = new
            // var transport =
            //     endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            // transport.ConnectionString(");

            //send remediate ack
            //log completed event
        }

        private string _queue = "policyctl.fixer.defaultservice";
        private ControllerSecret _secret;
        private IAzure _azure;
        private DefaultServicePlan _plan;
        private IPlanQueryManager _planManager;
        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
    }
}
