using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadDataAPI.Entities;
using LeadDataAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LeadDataAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticates a user (JWT authentication)
        /// </summary>
        /// <param name="user">Username and password details</param>
        /// <returns>Ok/Bad Request results</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User user)
        {
            var userDetails = _userService.Authenticate(user.Username, user.Password);

            if (userDetails == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userDetails);
        }
    }
}