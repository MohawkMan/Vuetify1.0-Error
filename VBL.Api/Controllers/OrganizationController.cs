using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using VBL.Core;
using Microsoft.AspNetCore.Authorization;
using VBL.Data.Mapping;
using Microsoft.Extensions.Configuration;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly OrganizationManager _organizationManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public OrganizationController(OrganizationManager organizationManager, IMapper mapper, ILogger<TournamentController> logger, IConfiguration configuration)
        {
            _organizationManager = organizationManager;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Create a new Organization
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{organizationId}")]
        [ProducesResponseType(typeof(OrganizationDTO), 200)]
        public async Task<IActionResult> GetOrganization([FromRoute] int organizationId)
        {
            try
            {
                _logger.LogInformation($"GetOrganization Id: {organizationId}");

                var organization = await _organizationManager.GetOrganizationAsync(organizationId);
                return Ok(organization);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Create a new Organization
        /// </summary>
        [HttpPut("Create")]
        [ProducesResponseType(typeof(OrganizationDTO), 200)]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDTO dto)
        {
            try
            {
                _logger.LogInformation($"CreateOrganization dto: {dto}");

                //if(!User.IsInRole("Admin"))

                var organization = await _organizationManager.CreateOrganizationAsync(dto);
                return Ok(organization);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Add a member to an organization
        /// </summary>
        [HttpPut("member")]
        [ProducesResponseType(typeof(OrganizationMemberDTO), 200)]
        public async Task<IActionResult> AddMemeber([FromBody] OrganizationMemberDTO dto)
        {
            try
            {
                _logger.LogInformation($"AddMemeber dto: {dto}");

                //if(!User.IsInRole("Admin"))

                var member = await _organizationManager.AddMemberAsync(dto);
                return Ok(member);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }
    }
}