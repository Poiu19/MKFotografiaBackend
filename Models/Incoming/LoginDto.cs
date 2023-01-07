using System.ComponentModel.DataAnnotations;

namespace MKFotografiaBackend.Models.Incoming
{
    public class LoginDto
    {
        [Required(ErrorMessage = "'Adres e-mail' jest wymaganym polem.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "'Hasło' jest wymaganym polem.")]
        public string Password { get; set; }
    }
}