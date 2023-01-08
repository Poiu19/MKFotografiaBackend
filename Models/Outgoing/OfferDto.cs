using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models.Outgoing
{
    public class OfferDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string RelativeURL { get; set; }
        public string Price { get; set; }
        public string FullOfferTeaser { get; set; }
        public string TeaserMobile { get; set; }
        public List<string> TeaserDesktop { get; set; }
        public List<string> BasicFeatures { get; set; }
        public List<string> AdditionalFeatures { get; set; }
        public virtual OfferDto? ConnectedOffer { get; set; }
        public virtual GalleryDto? ConnectedGallery { get; set; }
    }
}
