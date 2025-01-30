namespace Front.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Biogram { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public List<UserModel> FollowedUsers { get; set; } = new List<UserModel>();
    }
}
