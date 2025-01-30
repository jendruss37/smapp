namespace PostApi.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorUserName { get; set; }
    }
}
