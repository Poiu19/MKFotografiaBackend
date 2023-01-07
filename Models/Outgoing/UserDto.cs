using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Models.Outgoing
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public bool Active { set; get; }
        public int RoleId { set; get; }
        public RoleDto Role { set; get; }
        public DateTime? LastLogin { set; get; }
    }
}
