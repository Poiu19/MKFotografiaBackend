namespace MKFotografiaBackend.Entities
{
    public class GalleryPhoto
    {
        public int Id { get; set; }
        public string AlternativeText { get; set; }
        public string? Path { get; set; }
        public int GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}
