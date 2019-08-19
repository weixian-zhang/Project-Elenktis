namespace Elenktis.Assessment
{
    public class Policy
    {
        public Policy()
        {
        }

        [PolicyMeasure]
        public bool ToAssess { get; set; } = true;

        [PolicyMeasure()]
        public bool ToRemediate { get; set; } = true;

        [PolicyMeasure()]
        public bool ToNotify { get; set; } = false;
    }
}