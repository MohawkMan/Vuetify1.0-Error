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
using Microsoft.Extensions.Options;

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
        private readonly VblConfig _config;
        private readonly VblUserManager _userManager;

        public OrganizationController(OrganizationManager organizationManager, IMapper mapper, ILogger<TournamentController> logger, IOptions<VblConfig> config, VblUserManager userManager)
        {
            _organizationManager = organizationManager;
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets an Organization
        /// </summary>
        //[AllowAnonymous]
        //[HttpGet("{organizationId}")]
        //[ProducesResponseType(typeof(OrganizationDTO), 200)]
        //public async Task<IActionResult> GetOrganization([FromRoute] int organizationId)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"GetOrganization Id: {organizationId}");

        //        var organization = await _organizationManager.GetOrganizationAsync(organizationId);
        //        return Ok(organization);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(-1, e, "ERROR: ");
        //        return BadRequest(e.Message);
        //    }
        //}

        /// <summary>
        /// Gets an Organization
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{usernameOrId}")]
        [ProducesResponseType(typeof(OrganizationDTO), 200)]
        public async Task<IActionResult> GetOrganization([FromRoute] string usernameOrId)
        {
            try
            {
                _logger.LogInformation($"GetOrganization usernameOrId: {usernameOrId}");

                var id = 0;
                var publicOnly = true;
                var userId = 0;
                if (User != null)
                {
                    userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                }

                OrganizationDTO organization = null;

                if(int.TryParse(usernameOrId, out id))
                {
                    organization = await _organizationManager.GetOrganizationAsync(id);
                    publicOnly = !await _userManager.IsOrganizationMember(userId, id);
                }
                else
                {
                    organization = await _organizationManager.GetOrganizationAsync(usernameOrId);
                    publicOnly = !await _userManager.IsOrganizationMember(userId, usernameOrId);
                }

                if(publicOnly)
                {
                    var publicPhotos = organization.Photos.Where(w => w.IsPublic).ToList();
                    organization.Photos = publicPhotos;
                }
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