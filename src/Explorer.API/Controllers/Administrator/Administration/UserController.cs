using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Authorize(Policy = "administratorPolicy")]
    [Route("api/administration/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("{id:int}/block")]
        public ActionResult<UserDto> BlockUser(int id)
        {
            var result = _userService.Block(id);
            return CreateResponse(result);
        }

        [HttpGet("malicious")]
        public ActionResult<UserDto> GetMaliciousUsers()
        {
            var result = _userService.FindMaliciousUsers();
            return CreateResponse(result);
        }
    }
}
