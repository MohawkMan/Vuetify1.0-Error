using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VBL.Data;

namespace VBL.Web.SocialMediaPlatforms
{
    public class FacebookManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<FacebookManager> _logger;

        public FacebookManager(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<FacebookManager> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
    public async Task<IdentityResult> RegisterOnLogin(ExternalLoginInfo info)
        {
            var user = new ApplicationUser() {
                UserName = info.ProviderKey,
                RegistrationProvider = "Facebook"
            };
            #region Handle Claims
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
                user.FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Surname))
                user.LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Gender))
                user.Gender = info.Principal.FindFirstValue(ClaimTypes.Gender);

            user.NewNotification("Welcome you are now registered. Please complete your profile.", "go to profile");

            #endregion
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                ///HANDLE EMAIL
                ///
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                }
            }
            return result;
        }
    }
}
