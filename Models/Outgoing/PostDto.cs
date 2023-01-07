namespace MKFotografiaBackend.Models.Outgoing
{
    public class PostAnonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public PostDataDto? PostData { get; set; } = null;
    }
    public class PostDto : PostAnonDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public string? Path { get; set; } = null;
    }
}
