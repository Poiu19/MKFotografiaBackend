namespace MKFotografiaBackend.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RelativeURL { get; set; }
        public string Price { get; set; }
        public string TeaserMobile { get; set; }
        public List<string> TeaserDesktop { get; set; }
        public List<string> BasicFeatures { get; set; }
        public List<string> AdditionalFeatures { get; set; }
        public int? ConnectedOfferId { get; set; }
        public virtual Offer? ConnectedOffer { get; set; }
        public int? ConnectedGalleryId { get; set; } = null;
        public virtual Gallery? ConnectedGallery { get; set; }
    }
}
