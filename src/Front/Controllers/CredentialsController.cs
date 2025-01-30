using Flurl;
using Flurl.Http;
using Front.Models;
using Front.Models.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly string identityApiUrl = "http://identityapi:80/api/Identity";
                                                    
        public async Task<IActionResult> Login()
        {
          
                var testCredentials = new CredentialsDto { Password = "password1", UserName="user1"};
                
                var response = await identityApiUrl.AppendPathSegment("authenticate").PutJsonAsync(testCredentials);
                var result = response.GetStringAsync().Result;
                //dodać obsługę scenariuszy logowania
                var loginResult = new LoginResult { Result = result };

                return View(loginResult);
         

        }

        public async Task<IActionResult> RegisterUser(NewUserDto newUserDto)
        {
            var response = await identityApiUrl.AppendPathSegment("register").PostJsonAsync(newUserDto);
            var result = response.GetStringAsync().Result;
            //dodać obsługę scenariuszy rejestracji
            return View("Register");
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {
            var response = await identityApiUrl.AppendPathSegment("delete").SetQueryParam("userId", userId).DeleteAsync();
            return View("UserDeleted");
        }
    }
}
