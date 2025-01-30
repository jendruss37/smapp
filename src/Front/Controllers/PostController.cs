using Flurl;
using Flurl.Http;
using Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class PostController : Controller
    {
        private readonly string postApiUri = "http://postapi:80/api/Post";

        public async Task<IActionResult> GetPostsFromUser(int userId)
        {
            var response = await postApiUri.AppendPathSegment("getfromuser").SetQueryParam("userId", userId).GetJsonAsync<List<PostViewModel>>();
            return View(response);
        }
    }
}
