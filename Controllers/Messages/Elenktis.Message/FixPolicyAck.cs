// using System;
// using System.Text;

// namespace Elenktis.Message
// {
//     public class FixPolicyAck
//     {
//         public Guid CorrelationId { get; set; }

//         public string SubscriptionId { get; set; }

//         public DateTime TimeReceivedAtHandler { get; set; }
        
//         public bool IsRemediated { get; private set; }

//         public string ActivityPerformed { get; set; }

//         public void AddActivity(string activity)
//         {
//             var strBuilder = new StringBuilder(ActivityPerformed);
//             strBuilder.Append(activity);
//             strBuilder.AppendLine();
//             ActivityPerformed += strBuilder.ToString();
//         }

//         public void SetAcknowledge
//             (string subscriptionId, Guid correlationId, DateTime timeReceived)
//         {
//             SubscriptionId = subscriptionId;
//             CorrelationId = correlationId;
//             TimeReceivedAtHandler = timeReceived;
//         }

//         public void Remediated()
//         {
//             IsRemediated = true;
//         }
//     }
// }