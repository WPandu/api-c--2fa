using Microsoft.AspNetCore.Mvc;
using API2FA.Requests;
using API2FA.IServices;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using API2FA.Responses;
using API2FA.Models;

namespace API2FA.Controllers
{
    [ApiController]
    [Route("me")]
    public class MeController : ControllerBase
    {
        private IUserService _userService;

        public MeController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Me()
        {
            var user = (User?)HttpContext.Items["User"];
            return Ok(new SuccessResponse
            {
                Data = _userService.GetById(user.ID)
            });
        }
    }
}
