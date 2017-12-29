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

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly VblConfig _config;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(ApplicationUserManager userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config.Value;
        }

        [AllowAnonymous]
        [HttpGet("Test1")]
        public async Task<IActionResult> Test1()
        {
            return Ok("Test 1 Succeeded");
        }

        [HttpGet("Test2")]
        public async Task<IActionResult> Test2()
        {
            return Ok("Test 2 Succeeded");
        }

        [AllowAnonymous]
        [HttpPost("Test3")]
        public async Task<IActionResult> Test3()
        {
            return Ok("Test 3 Succeeded");
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
                        return Ok(await GenerateJwtToken(user));
                    }

                }
            }

            return BadRequest("Could not create token");
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.IdentityManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return GenerateJwtToken(user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async Task<VBLToken> GenerateJwtToken(ApplicationUser user)
        {
            var claims = await GetValidClaims(user);
            var isAdmin = await _userManager.IsInRoleAsync(user, "MohawkMan");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.AppKeys.Jwt));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires =  isAdmin ? DateTime.Now.AddMonths(6) : DateTime.Now.AddHours(3);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _config.Jwt.Issuer,
                _config.Jwt.Issuer,
                claims,
                expires: DateTime.Now.AddMonths(6),
                signingCredentials: creds
            );

            return new VBLToken { access_token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
        private async Task<List<Claim>> GetValidClaims(ApplicationUser user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };
            var userClaims = await _userManager.IdentityManager.GetClaimsAsync(user);
            var userRoles = await _userManager.IdentityManager.GetRolesAsync(user);
            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            return claims;
        }

        public class LoginDto
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }

        public class VBLToken
        {
            public string access_token { get; set; }
        }
    }
}
