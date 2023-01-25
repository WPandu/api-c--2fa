using Microsoft.AspNetCore.Mvc;
using API2FA.Requests;
using API2FA.IServices;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace API2FA.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            return Ok(_authenticationService.Login(loginRequest));
        }
    }
}
