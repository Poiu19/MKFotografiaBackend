namespace MKFotografiaBackend.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string PasswordHash { set; get; }
        public bool Active { set; get; } = true;
        public int RoleId { set; get; }
        public virtual Role Role { set; get; }
        public DateTime? LastLogin { set; get; } 
    }
}
