using Microsoft.AspNetCore.Http;
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
        //[HttpGet("get")]
        //public async Task<IActionResult> Get([FromQuery] int id)
        //{
        //    Post? result;
        //    try
        //    {
        //        result = await _postService.ge(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500);
        //    }

        //    return Ok(result);
        //}
    }
}
