using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;

namespace VBL.Core
{
    public partial class TournamentManager
    {
        public async Task LockResults(int tournamentDivisionId)
        {
            var division = await _db.TournamentDivisions
                .Include(i => i.Teams)
                    .ThenInclude(t => t.Players)
                .Include(i => i.Days)
                .Where(w => w.Id == tournamentDivisionId)
                .FirstOrDefaultAsync();
            if (division == null) return;

            var basePoints = await _db.PointValues
                .Where(w => w.DivisionId == division.DivisionId)
                .ToListAsync();
            if (!basePoints.Any()) return;

            var teamMultiplier = await _db.TeamCountMultipliers
                .OrderBy(o => o.TeamCap)
                .FirstOrDefaultAsync(f => f.TeamCap >= division.Teams.Count());

            foreach (var team in division.Teams)
            {
                var points = basePoints
                    .OrderBy(o => o.Finish)
                    .Where(w => w.Finish >= team.Finish || w.Finish > 10000)
                    .FirstOrDefault();

                if (points == null)
                    throw new Exception($"Couldn't match points TournamentDivisionId: {division.Id}, Division.Id: {division.DivisionId}, Finish: {team.Finish} ");

                foreach(var player in team.Players)
                {
                    player.Finish = team.Finish;
                    player.VblBasePointsEarned = points.Points;
                    player.DtEarned = division.Days.OrderByDescending(o => o.Date).First().Date;
                    player.DtFinalized = DateTime.Now;

                    if (player.Multipliers.Any())
                        player.Multipliers.Clear();

                    if(teamMultiplier != null)
                    {

                        if(points.Finish > 100000)
                        {
                            player.Multipliers.Add(new PointValueMultiplier
                            {
                                Type = "TeamCountMultiplier",
                                Description = $"{teamMultiplier.Description} NOT applied to participation points. Total teams: {division.Teams.Count()}, TeamCountMultiplier.Id: {teamMultiplier.Id}, Value: {teamMultiplier.Multiplier}",
                                Value = 1,
                                TournamentTeamMember = player
                            });
                            player.VblTotalPointsEarned = player.VblBasePointsEarned;
                        }
                        else
                        {
                            player.Multipliers.Add(new PointValueMultiplier
                            {
                                Type = "TeamCountMultiplier",
                                Description = $"{teamMultiplier.Description} applied. Total teams: {division.Teams.Count()}, TeamCountMultiplier.Id: {teamMultiplier.Id}, Value: {teamMultiplier.Multiplier}",
                                Value = teamMultiplier.Multiplier,
                                TournamentTeamMember = player
                            });
                            player.VblTotalPointsEarned = player.VblBasePointsEarned * teamMultiplier.Multiplier;
                        }
                    }
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
