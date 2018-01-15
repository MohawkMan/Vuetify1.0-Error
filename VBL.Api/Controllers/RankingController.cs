using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VBL.Core;
using VBL.Data;
using VBL.Data.Mapping;

namespace VBL.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RankingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly VblConfig _config;
        private readonly VBLDbContext _db;

        public RankingController(IMapper mapper, ILogger<RankingController> logger, IOptions<VblConfig> config, VBLDbContext db)
        {
            _mapper = mapper;
            _logger = logger;
            _config = config.Value;
            _db = db;
        }

        /// <summary>
        /// Get Player List With Points
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<PlayerPointsDTO>), 200)]
        public async Task<IActionResult> GetAllRankings()
        {
            var records = await _db.TournamentTeamMembers
                .Include(i => i.PlayerProfile)
                    .ThenInclude(t => t.User)
                .Select(s => new
                {
                    PlayerProfileId = s.PlayerProfileId,
                    Name = s.PlayerProfile.User == null ? $"{s.PlayerProfile.FirstName} {s.PlayerProfile.LastName}" : s.PlayerProfile.User.FullName,
                    Points = s.VblTotalPointsEarned
                })
                .ToListAsync();

            var grouped = records
                .GroupBy(g => new { g.PlayerProfileId, g.Name })
                .Select(s => new PlayerPointsDTO
                {
                    PlayerProfileId = s.Key.PlayerProfileId,
                    Name = s.Key.Name,
                    CurrentPoints = s.Sum(x => Convert.ToInt32(x.Points)),
                    Events = s.Count()
                })
                .ToList();

            //var ranks = grouped
            //    .GroupBy(r => r.CurrentPoints)
            //    .OrderBy(o => o.Key)
            //    .Select((s, i) => new { Points = s.Key, Rank = i + 1 })
            //    .ToList();

            //var results = grouped
            //    .Join(ranks, g => g.CurrentPoints, r => r.Points, (g, r) => new PlayerPointsDTO
            //    {
            //        PlayerProfileId = g.PlayerProfileId,
            //        Name = g.Name,
            //        CurrentPoints = g.CurrentPoints,
            //        CurrentRank = r.Rank
            //    })
            //    .ToList();

            return Ok(grouped.OrderByDescending(o => o.CurrentPoints));
        }

        /// <summary>
        /// Get Player List With Points
        /// </summary>
        [AllowAnonymous]
        [HttpGet("teams")]
        [ProducesResponseType(typeof(List<PlayerPointsDTO>), 200)]
        public async Task<IActionResult> GetAllTeamRankings()
        {
            var teams = await _db.TournamentTeams
                .Include(i => i.Players)
                    .ThenInclude(i => i.PlayerProfile)
                        .ThenInclude(t => t.User)
                .Where(w => w.TournamentDivision.Tournament.StatusId == 100)
                .ToListAsync();

            return Ok();
        }

        /// <summary>
        /// Get Point Scale
        /// </summary>
        [AllowAnonymous]
        [HttpGet("pointscale")]
        [ProducesResponseType(typeof(List<PlayerPointsDTO>), 200)]
        public async Task<IActionResult> GetPoints()
        {
            var points = await _db.PointValues
                .ProjectTo<PointValueDTO>()
                .ToListAsync();

            return Ok(points);
        }

        /// <summary>
        /// Get Team Multiplier
        /// </summary>
        [AllowAnonymous]
        [HttpGet("TeamMultiplier")]
        [ProducesResponseType(typeof(List<TeamCountMultiplierDTO>), 200)]
        public async Task<IActionResult> GetTeamMultiplier()
        {
            var points = await _db.TeamCountMultipliers
                .ProjectTo<TeamCountMultiplierDTO>()
                .ToListAsync();

            return Ok(points);
        }
    }
}
