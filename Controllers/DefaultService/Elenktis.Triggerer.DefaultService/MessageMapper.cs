// using AutoMapper;
// using Elenktis.Chassis.EventLogger.Event;

// namespace Elenktis.Saga.DefaultServiceSaga
// {
//     public class MessageMapper
//     {
//         public MessageMapper()
//         {
//             InitMapper();
//         }

//         public static IMapper CreateMapper()
//         {
//             return InitMapper();
//         }

//         private static IMapper InitMapper()
//         {
//             //NServiceBus disallow SagaData to be in different assembly.
//             //this is an alternate option to map data to SagaTrackingData for EventLogger
//             var config = new MapperConfiguration(cfg => {
//                 //cfg.CreateMap<DSSagaTrackingData, DSSagaEvent>();
//                 cfg.CreateMap<SagaStage, Elenktis.Chassis.EventLogger.Event.SagaStage>();
//             });

//             return config.CreateMapper();
//         }
//     }
// }