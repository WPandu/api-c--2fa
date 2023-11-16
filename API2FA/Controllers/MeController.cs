using Microsoft.AspNetCore.Mvc;
using API2FA.IServices;
using API2FA.Responses;
using API2FA.Models;

namespace API2FA.Controllers
{
    [ApiController]
    [Route("me")]
    public class MeController : ControllerBase
    {
        private readonly IUserService _userService;

        public MeController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Me()
        {
            var user = (User?)HttpContext.Items["User"];
            if (user == null) 
            {
                throw new System.UnauthorizedAccessException();
            }

            return Ok(new SuccessResponse
            {
                Data = _userService.GetById(user.ID)
            });
        }
    }
}
