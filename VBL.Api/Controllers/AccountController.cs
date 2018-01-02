using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.ComponentModel.DataAnnotations;
using VBL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using VBL.Core;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly VblUserManager _userManager;
        private readonly VblConfig _config;
        private readonly ILogger _logger;

        public AccountController(VblUserManager userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _config = config.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.IdentityManager.FindByNameAsync(model.UserName);

                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok(await _userManager.GenerateJwtToken(user));
                    }

                }
            }

            return BadRequest("Could not create token");
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                _logger.LogInformation($"Register dto: {JsonConvert.SerializeObject(model)}");
                var user = await _userManager.CreateAsync(model);
                await _signInManager.SignInAsync(user, false);
                var token = _userManager.GenerateJwtToken(user);
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
    }
}
