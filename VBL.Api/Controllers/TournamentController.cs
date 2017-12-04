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

        public TournamentController(TournamentManager tournamentManager, IMapper mapper, ILogger<TournamentController> logger)
        {
            _tournamentManager = tournamentManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get select options for Age, Gender, Division, and Location
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
        [HttpPost()]
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
    }
     public class TournamentSelectItems
    {
        public List<OptionDTO> AgeTypeOptions { get; set; }
        public List<OptionDTO> GenderOptions { get; set; }
        public List<OptionDTO> DivisionOptions { get; set; }
        public List<OptionDTO> LocationOptions { get; set; }
    }
}