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
using System.IO;

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
        private readonly VblUserManager _userManager;

        public TournamentController(TournamentManager tournamentManager, IMapper mapper, ILogger<TournamentController> logger, IOptions<VblConfig> config, VblUserManager userManager)
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
        [HttpGet("list")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetAllTournaments()
        {
            try
            {
                _logger.LogInformation($"GetAllTournaments");
                List<int> organizationIds = new List<int>();
                if (User != null)
                {
                    var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                    organizationIds = await _userManager.GetOrganizationIdsAsync(userId);
                }

                var list = await _tournamentManager.GetTournamentListAsync(organizationIds);
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
        /// Get Tournament Details
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}/raw")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetRawTournament([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"GetTournament Id: {id}");
                var tourney = await _tournamentManager.GetRawTournamentAsync(id);

                return Ok(tourney);
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
        [HttpGet("{id}/copy")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> GetTournamentCopy([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation($"GetTournament Id: {id}");
                var tourney = await _tournamentManager.GetTournamentCopyAsync(id);

                return Ok(tourney);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Put a new tournament or edits to an existing tournament
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> AddTournament([FromBody] TournamentDTOIncoming dto)
        {
            var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
            if (!await _userManager.CanEditOrganizationTournament(userId, dto.OrganizationId))
                return Unauthorized();

            try
            {
                _logger.LogInformation($"AddTournament: {dto}");
                if(dto.Id == 0)
                {
                    var newTournament = await _tournamentManager.CreateTournamentAsync(dto);

                    return Ok(newTournament);
                }
                var editedTournament = await _tournamentManager.EditTournamentAsync(dto);
                return Ok(editedTournament);
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
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> Register([FromBody]TournamentRegistrationWithPayDTO dto)
        {
            try
            {
                _logger.LogInformation($"Register TournamentRegistrationDTO: {dto}");
                var result = await _tournamentManager.Register(dto.Registration, true);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [HttpPut("register/upload/{overwrite?}")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> Register([FromBody]List<TournamentRegistrationDTO> dto, [FromRoute] bool overwrite = false)
        {
            try
            {
                // ADD SECURITY CHECKS
                _logger.LogInformation($"Register TournamentRegistrationDTO: {dto}");
                var result = await _tournamentManager.BulkRegister(dto, overwrite);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{tournamentId}/LockResults")]
        [ProducesResponseType(typeof(TournamentDTO), 200)]
        public async Task<IActionResult> LockResults([FromRoute] int tournamentId)
        {
            try
            {
                // ADD SECURITY CHECKS
                _logger.LogInformation($"LockResults tournamentId: {tournamentId}");
                await _tournamentManager.LockTournamentResults(tournamentId);

                return await GetTournament(tournamentId);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{tournamentId}/publish/{isPublic}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> Publish([FromRoute] int tournamentId, [FromRoute] bool isPublic)
        {
            try
            {
                var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                if (!await _userManager.CanEditTournament(userId, tournamentId))
                    return Unauthorized();

                _logger.LogInformation($"Publish tournamentId: {tournamentId}, isPublic: {isPublic}");
                await _tournamentManager.PublishAsync(tournamentId, isPublic);

                return Ok(isPublic);
            }
            catch (Exception e)
            {
                _logger.LogError(-1, e, "ERROR: ");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/seededDivisions")]
        [ProducesResponseType(typeof(List<TournamentDivisionDTO>), 200)]
        public async Task<IActionResult> GetRegistrations([FromRoute] int id)
        {
            try
            { 
                var userId = Convert.ToInt32(User.UserId(_config.Jwt.Issuer));
                if (!await _userManager.CanEditTournament(userId, id))
                    return Unauthorized();

                var divisions = await _tournamentManager.GetSeededDivisions(id);
                var result = _mapper.Map<List<TournamentDivisionTeams>>(divisions);
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
            try
            {
                //await _tournamentManager.LockResults(31);
                //await _tournamentManager.LockResults(33);
                //await _tournamentManager.LockResults(34);
                //await _tournamentManager.LockResults(35);
                //await _tournamentManager.LockResults(36);
            }
            catch (Exception e)
            {

            }
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