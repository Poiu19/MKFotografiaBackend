using System.ComponentModel.DataAnnotations;

namespace MKFotografiaBackend.Models.Incoming
{
    public class UploadSliderPhotoDto
    {
        [Required(ErrorMessage = "'Zdjęcie' jest wymaganym polem.")]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "'Kolejność' jest wymaganym polem.")]
        public int Order { get; set; }
        [Required(ErrorMessage = "'Tytuł' jest wymaganym polem.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "'Podtytuł' jest wymaganym polem.")]
        public string Subtitle { get; set; }
        [Required(ErrorMessage = "'Tekst alternatywny' jest wymaganym polem.")]
        public string AlternativeText { get; set; }
    }
}
