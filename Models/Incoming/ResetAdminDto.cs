using System.ComponentModel.DataAnnotations;

namespace MKFotografiaBackend.Models.Incoming
{
    public class ResetAdminDto
    {
        [Required(ErrorMessage = "'Hasło' jest wymaganym polem.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "'Powtórz hasło' jest wymaganym polem.")]
        public string ConfirmPassword { get; set; }
    }
}