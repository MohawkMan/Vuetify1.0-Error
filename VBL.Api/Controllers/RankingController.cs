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

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RankingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;

        public RankingController(IMapper mapper, ILogger<RankingController> logger, IOptions<VblConfig> config)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
        }

        /// <summary>
        /// Get Player List With Points
        /// </summary>
        [AllowAnonymous]
        [HttpGet("list/{organizationId?}")]
        //[ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetAllRankings()
        {
            return Ok();
        }
    }
}
