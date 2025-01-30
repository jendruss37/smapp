using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;

namespace PeopleApi.Services
{
    public interface IPeopleService
    {
        Task<UserInfo> GetUserInfo(int id);
        Task DeleteUserInfo(int id);
        Task AddNewUserInfo(UserInfo userInfo);
        Task<Dictionary<int, string>> GetUserNames(List<int> ids);
        Task<List<int>> GetFollowedIds(int userId);
    }
    public class PeopleService : IPeopleService
    {
        private readonly PeopleDbContext _context;
        public PeopleService(PeopleDbContext peopleDbContext)
        {
            _context = peopleDbContext;
        }
        public async Task<UserInfo> GetUserInfo(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("No user with given id");
            }
            return user;
        }
        public async Task DeleteUserInfo(int loginId)
        {
            var userInfo = await _context.Users.FindAsync(loginId);
            if (userInfo != null)
            {
                _context.Users.Remove(userInfo);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddNewUserInfo(UserInfo userInfo)
        {
            _context.Users.Add(userInfo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserInfo(UserInfo newUserInfo)
        {
            _context.Users.Update(newUserInfo);
            await _context.SaveChangesAsync();
        }


        public async Task<Dictionary<int, string>> GetUserNames(List<int> ids)
        {
            var result = await _context.Users.Where(u => ids.Contains(u.Id)).Select(u => new { u.Id, u.UserName }).ToDictionaryAsync(u => u.Id, u => u.UserName);
            return result;
        }
        public async Task<List<int>> GetFollowedIds(int userId)
        {
            var userFollows = await _context.UserFollows.Where(w => w.UserId == userId).ToListAsync();
            var result = userFollows.Select(u=>u.FollowedUserId).ToList();
            return result;
        }
    }
}
