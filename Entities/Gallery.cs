namespace MKFotografiaBackend.Entities
{
    public class Gallery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public virtual List<GalleryPhoto> Photos { get; set; }
    }
}
