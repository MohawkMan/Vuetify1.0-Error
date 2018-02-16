using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBL.Core;
using VBL.Data;
using VBL.Data.Mapping;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly VblUserManager _userManager;
        private readonly CartManager _cart;
        private readonly IHostingEnvironment _environment;

        public CartController(IMapper mapper, ILogger<TournamentController> logger, IOptions<VblConfig> config, VblUserManager userManager, CartManager cart, IHostingEnvironment environment)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _userManager = userManager;
            _cart = cart;
            _environment = environment;
        }

        [AllowAnonymous]
        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody] ShoppingBagDTO bag)
        {
            try
            {
                var skipPay = User.IsMohawkMan() && _environment.IsDevelopment();
                var order = await _cart.ProcessBag(bag, skipPay);
                var orderId = $"{order.DtCreated.Value.ToString("yyyyMMdd")}-{order.Id}";
                return Ok(orderId);
            }
            catch(StripeException e)
            {
                return BadRequest(e.StripeError);
            }
            catch(Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
    }
}
