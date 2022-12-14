using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models.Outgoing
{
    public class GalleryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public virtual List<GalleryPhotoDto> Photos { get; set; }
    }
}
