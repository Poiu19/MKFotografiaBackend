namespace MKFotografiaBackend.Models
{
    public class AuthenticationSettings
    {
        public string JwtKey { set; get; }
        public int JwtExpireDays { set; get; }
        public string JwtIssuer { set; get; }
    }
}
