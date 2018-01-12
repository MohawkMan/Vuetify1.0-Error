using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VBL.Core;
using VBL.Data;

namespace VBL.Api.Controllers
{
    [ApiVersionNeutral]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly VblUserManager _userManager;
        private readonly VblConfig _config;
        private readonly ILogger _logger;

        public AuthController(VblUserManager userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _config = config.Value;
        }

        [AllowAnonymous]
        [HttpGet("{provider}")]
        public async Task<IActionResult> ExternalLogin([FromRoute]string provider, [FromQuery]string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("Callback", "Auth", new { returnUrl = returnUrl }, Request.Scheme);
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        [HttpGet("Callback")]
        public async Task<IActionResult> Callback(string returnUrl = null, string remoteError = null)
        {
            var body = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }
            if (remoteError != null)
            {
                return BadRequest(new { Error = $"Error from external provider: {remoteError}" });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest(new { Error = $"Could not GetExternalLoginInfoAsync" });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                var user = await _userManager.IdentityManager.GetUserAsync(info.Principal);
                return Ok(await _userManager.GenerateJwtTokenAsync(user));
            }
            if (result.IsLockedOut)
            {
                return BadRequest(new { Error = $"Locked Out" });
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                var returnObj = new
                {
                    ReturnUrl = returnUrl,
                    LoginProvider = info.LoginProvider,
                    Email = info.Principal.HasClaim(c => c.Type == ClaimTypes.Email)? info.Principal.FindFirstValue(ClaimTypes.Email) : ""
                };
                return Ok(returnObj);
            }
        }

        [AllowAnonymous]
        [HttpGet("Confirm")]
        public async Task<IActionResult> OnPostConfirmationAsync([FromRoute]string email, [FromRoute]string returnUrl = null)
//        public async Task<IActionResult> OnPostConfirmationAsync([FromBody]RegisterViewModel model, [FromRoute]string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var model = new RegisterViewModel
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Email = email
                };
                var userResult = await _userManager.CreateAsync(model);
                if (userResult.Result.Succeeded)
                {
                    var result = await _userManager.IdentityManager.AddLoginAsync(userResult.User, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(userResult.User, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        var token = await _userManager.GenerateJwtTokenAsync(userResult.User);
                        return Ok(token);
                    }
                }
                foreach (var error in userResult.Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(new { Errors = ModelState, ReturnUrl = returnUrl });
        }

    }
}
