using Flurl;
using Flurl.Http;
using Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class PeopleController:Controller
    {
        private readonly string peopleApiUrl = "http://peopleapi:80/api/People";
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await peopleApiUrl.AppendPathSegment("get").SetQueryParam("id", userId).GetJsonAsync<UserModel>();
            return View(result);
        }

        public async Task<IActionResult> UpdateUser(UserModel user)
        {
            var result = await peopleApiUrl.AppendPathSegment("update").PutJsonAsync(user);
            if (result.StatusCode == 200)
            {
                return Ok();
            }

            throw new Exception("Error while communicating with PeopleApi");
        }
    }
}
