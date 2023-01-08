namespace MKFotografiaBackend.Entities
{
    public class SliderPhoto
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string AlternativeText { get; set; }
        public int Order { get; set; } = 0;
    }
}
