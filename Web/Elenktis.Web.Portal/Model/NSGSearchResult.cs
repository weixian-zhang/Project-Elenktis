namespace Elenktis.Web.Portal
{
    public class NSGSearchResult
    {
        public string NSGName { get; set; }
        public int SourcePort { get; set; }
        public string SourceAddress { get; set; }
        public int DestPort { get; set; }
        public string DestAddress { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }
}