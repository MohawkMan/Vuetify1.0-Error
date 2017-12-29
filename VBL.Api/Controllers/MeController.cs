using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using VBL.Data;
using VBL.Data.Mapping;
using VBL.Core;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace VBL.Api.Controllers
{
    /// <summary>
    /// Current logged in user controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MeController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;

        public MeController(ApplicationUserManager userManager, IMapper mapper, ILogger<MeController> logger, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
        }

        /// <summary>
        /// Gets the current logged in user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApplicationUserDTO), 200)]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                var myId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                var me = await _userManager.GetMe(myId);
                return Ok(me);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}