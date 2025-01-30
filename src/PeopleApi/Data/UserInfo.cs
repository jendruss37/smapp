namespace PeopleApi.Data
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Biogram { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public List<UserInfo> FollowedUsers { get; set; } = new List<UserInfo>();
    }
}
