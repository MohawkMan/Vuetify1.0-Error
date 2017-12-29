using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VBL.Data.Mapping;
using VBL.Core;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmailController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;

        public EmailController(ApplicationUserManager userManager, IMapper mapper, ILogger<MeController> logger, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
        }

        #region Me
        /// <summary>
        /// Update a email number of the current logged in user
        /// </summary>
        [HttpPost("Me")]
        [ProducesResponseType(typeof(EmailDTO), 200)]
        public async Task<IActionResult> UpdateEmail([FromBody] EmailDTO dto)
        {
            try
            {
                _logger.LogInformation($"UpdateEmail User.ID: {User.UserId(_config.Jwt.Issuer)}, dto: {JsonConvert.SerializeObject(dto)}");
                var email = await _userManager.UpdateEmailAsync(User, dto);
                return Ok(email);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a email number to the current logged in user
        /// </summary>
        [HttpPut("Me")]
        [ProducesResponseType(typeof(EmailDTO), 200)]
        public async Task<IActionResult> AddEmail([FromBody] EmailDTO dto)
        {
            try
            {
                _logger.LogInformation($"AddEmail User.ID: {User.UserId(_config.Jwt.Issuer)}, dto: {JsonConvert.SerializeObject(dto)}");
                var email = await _userManager.AddEmailAsync(User, dto);
                return Ok(email);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a email number from the current logged in user
        /// </summary>
        [HttpDelete("Me")]
        //[ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteEmail([FromBody] string address)
        {
            try
            {
                var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                _logger.LogInformation($"DeleteEmail User.ID: {userId}, Address: {address}");
                var result = await _userManager.DeleteEmailAsync(userId, address);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Update a email number for the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpPost("{UserId}")]
        [ProducesResponseType(typeof(EmailDTO), 200)]
        public async Task<IActionResult> UpdateEmail([FromBody] EmailDTO dto, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"UpdateEmail User.ID: {userId}, dto: {JsonConvert.SerializeObject(dto)}");
                var email = await _userManager.UpdateEmailAsync(userId, dto);
                return Ok(email);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a email number  for the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpPut("{UserId}")]
        [ProducesResponseType(typeof(EmailDTO), 200)]
        public async Task<IActionResult> AddEmail([FromBody] EmailDTO dto, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"AddEmail User.ID: {userId}, dto: {JsonConvert.SerializeObject(dto)}");
                var email = await _userManager.AddEmailAsync(userId, dto);
                return Ok(email);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a email number from the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpDelete("{UserId}")]
        //[ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteEmail([FromBody] string address, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"DeleteEmail User.ID: {userId}, Number: {address}");
                var result = await _userManager.DeleteEmailAsync(userId, address);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
    }
}