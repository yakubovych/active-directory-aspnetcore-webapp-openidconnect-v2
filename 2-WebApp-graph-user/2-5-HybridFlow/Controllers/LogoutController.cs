﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using WebApp_OpenIDConnect_DotNet.Services;

namespace WebApp_OpenIDConnect_DotNet.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly IConfidentialClientApplicationService _confidentialClientApplicationService;
        public LogoutController(IConfidentialClientApplicationService confidentialClientApplicationService)
        {
            _confidentialClientApplicationService = confidentialClientApplicationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync()
        {
            var accountId = User.GetMsalAccountId();

            if (!string.IsNullOrEmpty(accountId))
            {
                await _confidentialClientApplicationService.RemoveAccount(accountId);
                await HttpContext.SignOutAsync("Cookies");

                return Ok();
            }

            return Unauthorized();
        }
    }
}