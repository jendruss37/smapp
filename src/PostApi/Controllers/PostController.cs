using Microsoft.AspNetCore.Mvc;
using PostApi.Models;
using PostApi.Services;

namespace PostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("getfromuser")]
        public async Task<IActionResult> GetPostsFromUser([FromQuery] int userId)
        {
            List<PostViewModel> result;
            try
            {
                result = await _postService.GetPostsFromUser(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(result);
        }

        [HttpGet("getrecent")]
        public async Task<IActionResult> GetRecentPosts()
        {
            List<PostViewModel> result;
            try
            {
                result = await _postService.GetMostRecentPosts();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(result);
        }

        [HttpGet("getfromuserfollows")]
        public async Task<IActionResult> GetPostsFromFollowedUsers([FromQuery] int userId)
        {
            List<PostViewModel> result;
            try
            {
                result = await _postService.GetPostsFromFollowedUsers(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewPost(Post post)
        {
            try
            {
                await _postService.AddPost(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                await _postService.DeletePost(postId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
    }
}