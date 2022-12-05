namespace MKFotografiaBackend.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public DateTime? LastLogin { set; get; } = null;
    }
}
