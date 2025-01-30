using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.Models;

namespace PostApi.Services
{
    public interface IPostService
    {
        Task<List<PostViewModel>> GetPostsFromUser(int userId);
        Task<List<PostViewModel>> GetMostRecentPosts();
        Task<List<PostViewModel>> GetPostsFromFollowedUsers(int userId);
        Task DeletePost(int postId);
        Task AddPost(Post post);
    }
    public class PostService : IPostService
    {
        private readonly PostDbContext _dbContext;
        public PostService(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<PostViewModel>> GetPostsFromUser(int userId)
        {
            var posts = await _dbContext.Posts.Where(p => p.UserId == userId).ToListAsync();
            var postViewModelList = await ConvertPostsToPostViewModels(posts);
            return postViewModelList;

        }

        public async Task<List<PostViewModel>> GetMostRecentPosts()
        {
            var posts = await _dbContext.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
            var postViewModelList = await ConvertPostsToPostViewModels(posts);
            return postViewModelList;
        }

        public async Task<List<PostViewModel>> GetPostsFromFollowedUsers(int userId)
        {
            var followedUsersIds = await $"http://people_api:80/api/People/followedids?userId={userId}".GetJsonAsync<List<int>>();
            var posts = await _dbContext.Posts.Where(p => followedUsersIds.Contains(p.UserId)).ToListAsync();
            var postViewModelList = await ConvertPostsToPostViewModels(posts);
            return postViewModelList;
        }
        public async Task DeletePost(int postId)
        {
            var post = await _dbContext.Posts.FindAsync(postId);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task AddPost(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }




        private async Task<List<PostViewModel>> ConvertPostsToPostViewModels(List<Post> posts)
        {
            var userIds = posts.Select(p => p.UserId).ToList();
            var userNamesDict = await "http://people_api:80/api/People/usernames".PostJsonAsync(userIds).ReceiveJson<Dictionary<int, string>>();
            var result = new List<PostViewModel>();

            foreach (var post in posts)
            {
                result.Add(new PostViewModel
                {
                    Id = post.Id,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId,
                    CreatorUserName = userNamesDict[post.UserId],
                });
            }
            return result;
        }
    }
}