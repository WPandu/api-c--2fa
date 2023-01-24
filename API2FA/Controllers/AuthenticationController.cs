using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace API2FA.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpPost("accesstoken", Name = "login")]
        public IActionResult Login([FromBody] Authentication auth)
        {
            try
            {
                return Ok(_userService.Login(auth));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
