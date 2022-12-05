using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models
{
    public class OfferDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Price { get; set; }
        public List<string> BasicFeatures { get; set; }
        public List<string> AdditionalFeatures { get; set; }
        public virtual Offer? ConnectedOffer { get; set; }
    }
}
