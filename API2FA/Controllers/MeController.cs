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
    [Route("me")]
    public class MeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Me()
        {
            return Ok(HttpContext.Items["User"]);
        }
    }
}
