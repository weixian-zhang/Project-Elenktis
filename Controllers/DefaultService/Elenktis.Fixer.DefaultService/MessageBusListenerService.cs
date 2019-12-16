// using System.Threading;
// using System.Threading.Tasks;
// using Elenktis.Message.DefaultService;
// using Elenktis.Policy;
// using Elenktis.Secret;
// using Microsoft.Extensions.Hosting;
// using NServiceBus;
// using Elenktis.Message;

// namespace Elenktis.Fixer.DefaultServiceFixer
// {
//     public class MessageBusListenerService : BackgroundService
//     {
//         public MessageBusListenerService()
//         {
//         }

//         protected async override Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//            while(!stoppingToken.IsCancellationRequested)
//            {
//                await Task.Delay(Timeout.Infinite, stoppingToken);
//            }
//         }

        




        
//         private IPlanQueryManager _planQueryManager;

//         private DSFixerSecret _secrets;

//     }

// }