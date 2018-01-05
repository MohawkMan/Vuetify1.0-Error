using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using Hangfire;


namespace VBL.Core
{
    public partial class VblUserManager
    {
        public async Task<IdentityResultWithUser> CreateAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email
            };
            var email = new UserEmail
            {
                Address = model.Email,
                IsPrimary = true,
                IsPublic = false,
                IsVerified = false
            };
            user.UserEmails.Add(email);
            var result = await IdentityManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                //Send Welcome Email
                BackgroundJob.Enqueue<EmailManager>(em => em.SendSiteRegistrationEmailAsync(email.Id));
            }
            return new IdentityResultWithUser
            {
                Result = result,
                User = user
            };
        }
        public async Task<VBLToken> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = await GetValidClaimsAsync(user);
            var isAdmin = await IdentityManager.IsInRoleAsync(user, "MohawkMan");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.AppKeys.Jwt));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = isAdmin ? DateTime.Now.AddMonths(6) : DateTime.Now.AddHours(3);
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
        private async Task<List<Claim>> GetValidClaimsAsync(ApplicationUser user)
        {
            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };
            var userClaims = await IdentityManager.GetClaimsAsync(user);
            var userRoles = await IdentityManager.GetRolesAsync(user);
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

    }
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
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

    public class IdentityResultWithUser
    {
        public IdentityResult Result { get; set; }
        public ApplicationUser User { get; set; }
    }
}
