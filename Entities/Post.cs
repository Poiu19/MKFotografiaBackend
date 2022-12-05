using Microsoft.AspNetCore.SignalR;

namespace MKFotografiaBackend.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public string? Path { get; set; }
    }
}
