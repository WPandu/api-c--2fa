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
        private IMeService _meService;

        public MeController(IMeService MeService) {
            _meService = MeService;
        }

        [HttpGet]
        public IActionResult me()
        {
            return Ok(_meService.Me());
        }
    }
}
