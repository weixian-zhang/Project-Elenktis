using System.Threading.Tasks;
using Elenktis.Azure;
using Elenktis.Message.DefaultService;
using Elenktis.Policy;
using Microsoft.Azure.Management.Security.Models;
using NServiceBus;

namespace Elenktis.Spy.DefaultServiceSpy
{
    public class AssessASCAutoEnableVMHandler : IHandleMessages<AssessASCAutoRegisterVM>
    {
        public AssessASCAutoEnableVMHandler(IPlanQueryManager planManager, IAzure azure)
        {
            _planManager = planManager;
            _azure = azure;
        }

        public async Task Handle(AssessASCAutoRegisterVM message, IMessageHandlerContext context)
        {
            // var asc = _azure.SecurityCenterClient;

            // AutoProvisioningSetting  aps =
            //     await asc.AutoProvisioningSettings.GetAsync("default");

            // if(aps.AutoProvision == "Off")
            // {
            //     var comm = new EnableASCAutoRegisterVMCommand()
            //     {
            //         //Action = DefaultServiceFixerAction.EnableASCAutoRegisterVM,
            //         AutoProvision = false,
            //     };
                
            //     //TODO: send command to Saga
            //     await _bus.Send(QueueDirectory.Fixer.DefaultServiceSaga.StartSagaQueue
            //     , JsonConvert.SerializeObject(comm));
            // }
            // else
            // {
            //     //todo log activity to cosmos
            // }
        }

        private IPlanQueryManager _planManager;

        private IAzure _azure;
    }
}