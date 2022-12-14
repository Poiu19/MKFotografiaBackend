using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models.Outgoing
{
    public class GalleryPhotoDto
    {
        public int Id { get; set; }
        public string AlternativeText { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}
