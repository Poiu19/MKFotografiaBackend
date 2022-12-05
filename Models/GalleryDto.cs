using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models
{
    public class GalleryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public virtual List<GalleryPhoto> Photos { get; set; }
    }
}
