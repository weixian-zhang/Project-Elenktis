using System.ComponentModel.DataAnnotations;

namespace Elenktis.Web.Api
{
    public class SubscriptionModel
    {
        [Required]
        public string subscription { get; set; }

        [Required]
        public string plan { get; set; }

        public string policy { get; set; }

        public string measure { get; set; }
        public bool enabled { get; set; }
    }
}
