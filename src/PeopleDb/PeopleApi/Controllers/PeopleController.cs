using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data;
using PeopleApi.Services;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;

        }
        [HttpGet("get")]
        public async Task<IActionResult> GetUserInfo([FromQuery] int id)
        {
            UserInfo? result;
            try
            {
                result = await _peopleService.GetUserInfo(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return Ok(result);
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfo info)
        {
            try
            {
                await _peopleService.UpdateUserInfo(info);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        #region internal_microservices_communication

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserInfo([FromQuery] int loginId)
        {
            try
            {
                await _peopleService.DeleteUserInfo(loginId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewUserInfo([FromBody] UserInfo userInfo)
        {
            try
            {
                await _peopleService.AddNewUserInfo(userInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(userInfo);
        }

        [HttpPost("usernames")]
        public async Task<IActionResult> GetUserNames([FromBody]List<int> ids)
        {
            Dictionary<int, string> result;
            try
            {
                result = await _peopleService.GetUserNames(ids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(result);
        }
        [HttpGet("followedids")]
        public async Task<IActionResult> GetFollowedIds([FromQuery] int userId)
        {
            List<int> result;
            try
            {
                result = await _peopleService.GetFollowedIds(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(result);
        }
#endregion
    }
}
