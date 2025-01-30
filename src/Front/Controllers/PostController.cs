using Flurl;
using Flurl.Http;
using Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class PostController : Controller
    {
        private readonly string postApiUri = "http://postapi:80/api/Post";

        public async Task<IActionResult> GetPost()
        {
            var response = await postApiUri.AppendPathSegment("getfromuser").SetQueryParam("userId", 1).GetJsonAsync<List<PostViewModel>>();
            return View(response.FirstOrDefault());
        }
    }
}
