using System.ComponentModel.DataAnnotations;

namespace MKFotografiaBackend.Models.Incoming
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "'Adres e-mail' jest wymaganym polem.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "'Rola' jest wymaganym polem.")]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "'Imię' jest wymaganym polem.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "'Nazwisko' jest wymaganym polem.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "'Hasło' jest wymaganym polem.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "'Powtórz hasło' jest wymaganym polem.")]
        public string ConfirmPassword { get; set; }
    }
}
