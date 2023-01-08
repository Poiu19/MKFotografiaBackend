namespace MKFotografiaBackend.Models.Outgoing
{
    public class SliderPhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string AlternativeText { get; set; }
        public int Order { get; set; }
    }
}
