namespace Elenktis.Saga.DefaultServiceSaga
{
    public class TriggererSecret
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string ASBHost { get; set; }

        public string ASBSASKeyName { get; set; }

        public string ASBSASKeyValue { get; set; }

        public string EtcdHost { get; set; }

        public string EtcdPort { get; set; }
    }
}