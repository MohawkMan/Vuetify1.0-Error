using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBL.Core;
using VBL.Data;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PayPalController : Controller
    {
        private readonly VblConfig _config;
        private readonly ILogger _logger;

        [AllowAnonymous]
        [HttpPost("success")]
        public async Task<IActionResult> PaypalSuccess([FromForm] PayPalPaymentResponse response)
        {
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("error")]
        public async Task<IActionResult> PaypalError([FromForm] PayPalPaymentResponse response)
        {
            //save response
            //return to register/now
            return Ok();
        }
    }
}
