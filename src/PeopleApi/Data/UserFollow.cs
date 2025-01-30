namespace PeopleApi.Data
{
    public class UserFollow
    {
        public int Id {  get; set; }
        public int UserId {  get; set; }
        public int FollowedUserId {  get; set; }
    }
}
