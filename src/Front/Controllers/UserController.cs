using Flurl.Http;
using Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class UserController:Controller
    {
        public async Task<IActionResult> GetUser()
        {
            var apiUrl = $"http://people_api:80/api/People/get?id=1";
            var result = await apiUrl.GetJsonAsync<UserModel>();
            return View(result);
        }
    }
}
