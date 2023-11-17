using Microsoft.AspNetCore.Mvc;
using API2FA.Requests;
using API2FA.Models;
using API2FA.IServices;
using API2FA.Responses;

namespace API2FA.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

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

        [HttpGet("2fa-qr-code")]
        public IActionResult GoogleQrCode()
        {
            var user = (User?)HttpContext.Items["User"];
            return Ok(new SuccessResponse
            {
                Data = _authenticationService.GoogleQrCode(user)
            });
        }

        [HttpPost("2fa-qr-code")]
        public IActionResult RegisterGoogleQrCode(RegisterGoogleQrCodeRequest registerGoogleQrCodeRequest)
        {
            var user = (User?)HttpContext.Items["User"];
            if (user == null) 
            {
                throw new System.UnauthorizedAccessException();
            }
            _authenticationService.RegisterGoogleQrCode(user.ID, registerGoogleQrCodeRequest);
            return Ok(new SuccessResponse
            {
                Message = "Register Google QR Code Successfully"
            });
        }
    }
}
