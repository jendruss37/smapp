using IdentityApi.Models;
using IdentityApi.Services;
using IdentityApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] NewUserDto newUserDto)
        {
            var result = await _identityService.RegisterNewUser(newUserDto);
            if (result != RegistrationResult.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] CredentialsDto credentials)
        {
            var result = await _identityService.AuthenticateUser(credentials);
            if (result != AuthenticationResult.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int userId)
        {
            try
            {
                await _identityService.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
