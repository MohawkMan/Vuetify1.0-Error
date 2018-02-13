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
    public class AAUController : Controller
    {
        private readonly AAUManager _aau;
        private readonly VblConfig _config;
        private readonly ILogger _logger;

        public AAUController(AAUManager aau, IOptions<VblConfig> config, ILogger<AAUController> logger)
        {
            _aau = aau;
            _config = config.Value;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] AAUDTO dto)
        {
            try
            {
                _logger.LogInformation($"Verify AAU Number");
                var result = false;
                switch(dto.By.ToLower())
                {
                    case "zip":
                    case "zipcode":
                        result = await _aau.VerifyByZipAsync(dto.Id, dto.Zipcode);
                        break;
                    case "dob":
                        result = await _aau.VerifyByDobAsync(dto.Id, dto.Dob);
                        break;
                    default:
                        result = await _aau.VerifyByLastNameAsync(dto.Id, dto.Lastname);
                        break;
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
    }
    public class AAUDTO
    {
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Zipcode { get; set; }
        public string Dob { get; set; }
        public string By { get; set; }
    }
}
