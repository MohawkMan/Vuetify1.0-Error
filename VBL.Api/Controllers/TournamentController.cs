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

        //GET age/gender/division/location for an organizer
        [HttpGet("Selects/{organizationId?}")]
        [ProducesResponseType(typeof(TournamentSelectItems), 200)]
        public async Task<IActionResult> GetSelectItemsAsync([FromRoute] int? organizationId = null)
        {
            try
            {
                _logger.LogInformation($"GetSelectItemsAsync organizationId: {organizationId}");
                var result = new TournamentSelectItems
                {
                    AgeTypeOptions = await _tournamentManager.GetAllAgeTypesAsync(),
                    GenderOptions = await _tournamentManager.GetAllGendersAsync(),
                    DivisionOptions = await _tournamentManager.GetAllDivisionsAsync(),
                    LocationOptions = await _tournamentManager.GetOrganizationLocationsAsync(organizationId)
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
        public List<AgeType> AgeTypeOptions { get; set; }
        public List<Gender> GenderOptions { get; set; }
        public List<Division> DivisionOptions { get; set; }
        public List<LocationDTO> LocationOptions { get; set; }
    }
}