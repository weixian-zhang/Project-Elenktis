using System.Threading.Tasks;
using Elenktis.Policy;
using Elenktis.Policy.DefaultService;

namespace Elenktis.Fixers.DefaultServiceFixer
{
    public class EnableASCAutoVMConsumer //: IConsumer<EnableASCAutoRegisterVMCommand>
    {
        public EnableASCAutoVMConsumer(DefaultServicePlan queryManager)
        {
            _plan = queryManager;
        }

        // public async Task Consume(ConsumeContext<EnableASCAutoRegisterVMCommand> context)
        // {
        //     IPlanQueryManager _planManager = new PlanQueryManager();

        //     var dsp =
        //         await _planManager.GetDefaultServicePlansAsync(context.Message.SubscriptionId);
            
        //     if(dsp.ASCAutoRegisterVMEnabledPolicy.ToRemediate)
        //     {
                
        //     }
            
        //     throw new System.NotImplementedException();
        // }

        private DefaultServicePlan _plan;

    }
}