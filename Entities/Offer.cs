namespace MKFotografiaBackend.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Price { get; set; }
        public List<string> BasicFeatures { get; set; }
        public List<string> AdditionalFeatures { get; set; }
        public int? ConnectedOfferId { get; set; }
        public virtual Offer? ConnectedOffer { get; set; }
    }
}
