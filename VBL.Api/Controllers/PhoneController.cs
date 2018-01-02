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
    public class PhoneController : Controller
    {
        private readonly VblUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;

        public PhoneController(VblUserManager userManager, IMapper mapper, ILogger<MeController> logger, IOptions<VblConfig> config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
        }

        #region Me
        /// <summary>
        /// Update a phone number of the current logged in user
        /// </summary>
        [HttpPost("Me")]
        [ProducesResponseType(typeof(PhoneDTO), 200)]
        public async Task<IActionResult> UpdatePhone([FromBody] PhoneDTO dto)
        {
            try
            {
                _logger.LogInformation($"UpdatePhone User.ID: {User.UserId(_config.Jwt.Issuer)}, dto: {JsonConvert.SerializeObject(dto)}");
                var phone = await _userManager.UpdatePhoneAsync(User, dto);
                return Ok(phone);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a phone number to the current logged in user
        /// </summary>
        [HttpPut("Me")]
        [ProducesResponseType(typeof(PhoneDTO), 200)]
        public async Task<IActionResult> AddPhone([FromBody] PhoneDTO dto)
        {
            try
            {
                _logger.LogInformation($"AddPhone User.ID: {User.UserId(_config.Jwt.Issuer)}, dto: {JsonConvert.SerializeObject(dto)}");
                var phone = await _userManager.AddPhoneAsync(User, dto);
                return Ok(phone);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a phone number from the current logged in user
        /// </summary>
        [HttpDelete("Me")]
        //[ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeletePhone([FromBody] string number)
        {
            try
            {
                var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                _logger.LogInformation($"DeletePhone User.ID: {userId}, Number: {number}");
                var result = await _userManager.DeletePhoneAsync(userId, number);
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
        /// Update a phone number for the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpPost("{UserId}")]
        [ProducesResponseType(typeof(PhoneDTO), 200)]
        public async Task<IActionResult> UpdatePhone([FromBody] PhoneDTO dto, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"UpdatePhone User.ID: {userId}, dto: {JsonConvert.SerializeObject(dto)}");
                var phone = await _userManager.UpdatePhoneAsync(userId, dto);
                return Ok(phone);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a phone number  for the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpPut("{UserId}")]
        [ProducesResponseType(typeof(PhoneDTO), 200)]
        public async Task<IActionResult> AddPhone([FromBody] PhoneDTO dto, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"AddPhone User.ID: {userId}, dto: {JsonConvert.SerializeObject(dto)}");
                var phone = await _userManager.AddPhoneAsync(userId, dto);
                return Ok(phone);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a phone number from the given userId
        /// </summary>
        [Authorize(Roles = "MohawkMan,Admin")]
        [HttpDelete("{UserId}")]
        //[ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeletePhone([FromBody] string number, [FromRoute] int userId)
        {
            try
            {
                _logger.LogInformation($"DeletePhone User.ID: {userId}, Number: {number}");
                var result = await _userManager.DeletePhoneAsync(userId, number);
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