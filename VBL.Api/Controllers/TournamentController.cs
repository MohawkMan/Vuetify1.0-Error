using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VBL.Core;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VBL.Data;
using VBL.Data.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TournamentController : Controller
    {
        private readonly TournamentManager _tournamentManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly ApplicationUserManager _userManager;

        public TournamentController(TournamentManager tournamentManager, IMapper mapper, ILogger<TournamentController> logger, IOptions<VblConfig> config, ApplicationUserManager userManager)
        {
            _tournamentManager = tournamentManager;
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// Get Tournament List
        /// </summary>
        [AllowAnonymous]
        [HttpGet("list/{organizationId?}")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetAllTournaments([FromRoute] int? organizationId)
        {
            try
            {
                _logger.LogInformation($"GetAllTournaments");
                List<TournamentDTO> list;
                if (organizationId.HasValue)
                {
                    _logger.LogInformation($"GetAllTournaments - organizationId:{organizationId}");
                    var publicOnly = true;
                    if (User != null)
                    {
                        var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                        publicOnly = !await _userManager.IsOrganizationMember(userId, organizationId.Value);
                    }
                    list = await _tournamentManager.GetTournamentListAsync(publicOnly, organizationId);

                    return Ok(list);
                }

                list = await _tournamentManager.GetTournamentListAsync(true, organizationId);

                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Tournament List
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{organizationUsername}/list")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetOrganizationTournaments([FromRoute] string organizationUsername)
        {
            try
            {
                _logger.LogInformation($"GetOrganizationTournaments");
                List<TournamentDTO> list;
                if (!string.IsNullOrWhiteSpace(organizationUsername))
                {
                    _logger.LogInformation($"GetOrganizationTournaments - username: {organizationUsername}");
                    var publicOnly = true;
                    if (User != null)
                    {
                        var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                        publicOnly = !await _userManager.IsOrganizationMember(userId, organizationUsername);
                    }
                    list = await _tournamentManager.GetTournamentListAsync(publicOnly, organizationUsername);

                    return Ok(list);
                }

                list = await _tournamentManager.GetTournamentListAsync(true, organizationUsername);

                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Tournament Details
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetTournament([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"GetTournament Id: {id}");
                var tourney = await _tournamentManager.GetTournamentAsync(id);

                return Ok(tourney);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get select options for Age, Gender, Division, and Location
        /// </summary>
        [AllowAnonymous]
        [HttpPut()]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> AddTournament([FromBody] TournamentDTO dto)
        {
            try
            {
                _logger.LogInformation($"AddTournament: {dto}");
                var tourney = await _tournamentManager.CreateTournamentAsync(dto);

                return Ok(tourney);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Get select options for Age, Gender, Division, and Location
        /// </summary>
        [AllowAnonymous]
        [HttpGet("Selects/{organizationId?}")]
        [ProducesResponseType(typeof(TournamentSelectItems), 200)]
        public async Task<IActionResult> GetSelectItemsAsync([FromRoute] int? organizationId = null)
        {
            try
            {
                _logger.LogInformation($"GetSelectItemsAsync organizationId: {organizationId}");
                var result = new TournamentSelectItems
                {
                    AgeTypeOptions = await _tournamentManager.GetAllAgeTypeOptionsAsync(),
                    GenderOptions = await _tournamentManager.GetAllGenderOptionsAsync(),
                    DivisionOptions = await _tournamentManager.GetAllDivisionOptionsAsync(),
                    LocationOptions = await _tournamentManager.GetOrganizationLocationOptionsAsync(organizationId)
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody]TournamentRegistrationDTO dto)
        {
            try
            {
                _logger.LogInformation($"Register TournamentRegistrationDTO: {dto}");
                var result = await _tournamentManager.Register(dto);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("test")]
        public async Task<IActionResult> Test()
        {

            return Ok();
        }
    }
    public class TournamentSelectItems
    {
        public List<OptionDTO> AgeTypeOptions { get; set; }
        public List<Option2DTO> GenderOptions { get; set; }
        public List<Option2DTO> DivisionOptions { get; set; }
        public List<OptionDTO> LocationOptions { get; set; }
    }
}