using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBL.Core;
using Stripe;


namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StripeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly VblUserManager _userManager;
        private readonly StripeManager _stripe;

        public StripeController(IMapper mapper, ILogger<TournamentController> logger, IOptions<VblConfig> config, VblUserManager userManager, StripeManager stripe)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _userManager = userManager;
            _stripe = stripe;
        }

        /// <summary>
        /// Connect Stripe account with Oauth redirects
        /// </summary>
        [HttpGet("connect/{organizationId}")]
        public async Task<IActionResult> Connect([FromRoute] int organizationId)
        {
            var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
            if (! await _userManager.IsOrganizationMember(userId, organizationId))
                return Unauthorized();

            return Ok(await _stripe.GetConnectURL(userId, organizationId));
        }

        [AllowAnonymous]
        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery] string scope, [FromQuery] string code, [FromQuery] string state, [FromQuery] string error, [FromQuery] string error_description)
        {
           var returnUrl = _config.BaseURL;
           try
            {
                if (!string.IsNullOrWhiteSpace(error))
                {
                    returnUrl = $"{await _stripe.GetReturnURL(state)}?error={error}&error_description={error_description}";
                }
                else
                {
                    returnUrl = await _stripe.Connect(code, state, scope);
                }
                return Redirect(returnUrl);
            }
            catch(StripeException e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                returnUrl = $"{await _stripe.GetReturnURL(state)}?error={e.StripeError.Error}&error_description={e.StripeError.ErrorDescription}";
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                returnUrl = $"{await _stripe.GetReturnURL(state)}?error=unknown";
                return Redirect(returnUrl);
            }
        }
    }
}
