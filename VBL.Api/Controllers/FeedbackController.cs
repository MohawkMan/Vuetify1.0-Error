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
using VBL.Data;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly VBLDbContext _db;

        public FeedbackController(IMapper mapper, ILogger<RankingController> logger, IOptions<VblConfig> config, VBLDbContext db)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _db = db;
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> PutFeedbackAsync([FromBody] Feedback feedback)
        {
            _db.Feedback.Add(feedback);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
