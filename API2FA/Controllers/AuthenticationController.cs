using Microsoft.AspNetCore.Mvc;
using API2FA.Requests;
using API2FA.Models;
using API2FA.IServices;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using API2FA.Responses;

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
            
            return Ok(new SuccessResponse
            {
                Data = _authenticationService.Login(loginRequest),
            });
        }

        [HttpGet("google-qr-code")]
        public IActionResult GoogleQrCode()
        {
            var user = (User?)HttpContext.Items["User"];
            return Ok(new SuccessResponse
            {
                Data = _authenticationService.GoogleQrCode(user)
            });
        }

        [HttpPost("google-qr-code")]
        public IActionResult RegisterGoogleQrCode(RegisterGoogleQrCodeRequest registerGoogleQrCodeRequest)
        {
            var user = (User?)HttpContext.Items["User"];
            _authenticationService.RegisterGoogleQrCode(user.ID, registerGoogleQrCodeRequest);
            return Ok(new SuccessResponse
            {
                Message = "Register Google QR Code Successfully"
            });
        }
    }
}
