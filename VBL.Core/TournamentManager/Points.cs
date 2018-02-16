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
        public async Task<List<TournamentDivision>> GetSeededDivisions(int tournamentId)
        {
            var divisions = await _db.TournamentDivisions
                .Include(i => i.Teams)
                    .ThenInclude(t => t.Players)
                .Include(i => i.Days)
                .Where(w => w.TournamentId == tournamentId)
                .Where(w => w.Teams.Any(a => !a.IsDeleted))
                .ToListAsync();

            foreach (var division in divisions)
            {
                await SeedDivision(division);
            }
            return divisions;
        }
        public async Task SeedDivision(TournamentDivision division)
        {
            // Load teams
            if (!_db.Entry(division).Collection(c => c.Teams).IsLoaded)
                await _db.Entry(division).Collection(c => c.Teams).LoadAsync();
            // If no team we are done
            if (!division.Teams.Any())
                return;
            // Load days 
            if (!_db.Entry(division).Collection(c => c.Days).IsLoaded)
                await _db.Entry(division).Collection(c => c.Days).LoadAsync();
            // Get 1st day as start date
            var divisionStartDate = division.Days.OrderBy(o => o.Date).First().Date;
            var pointWindowStartDate = divisionStartDate.AddYears(-1);

            foreach(var team in division.Teams.Where(w => !w.IsDeleted))
            {
                // Load team members
                if (!_db.Entry(team).Collection(c => c.Players).IsLoaded)
                    await _db.Entry(team).Collection(c => c.Players).LoadAsync();

                foreach (var player in team.Players)
                {
                    // Load player profile
                    if (!_db.Entry(player).Reference(r => r.PlayerProfile).IsLoaded)
                        await _db.Entry(player).Reference(r => r.PlayerProfile).LoadAsync();

                    var points = await _db.TournamentTeamMembers
                        .Where(w => w.PlayerProfileId == player.PlayerProfileId)
                        .Where(w => w.DtEarned >= pointWindowStartDate)
                        .Where(w => w.DtEarned < divisionStartDate)
                        .ToListAsync();

                    player.AauSeedingPoints = points.Where(w => w.SanctioningBodyId == "AAU").Sum(s => s.VblTotalPointsEarned);
                    player.AvpSeedingPoints = points.Where(w => w.SanctioningBodyId.StartsWith("AVP")).Sum(s => s.VblTotalPointsEarned);
                }
            }
        }
        public async Task SeedDivision(int tournamentDivisionId)
        {
            var division = await _db.TournamentDivisions
                .Include(i => i.Teams.Where(w => !w.IsDeleted))
                    .ThenInclude(t => t.Players)
                .Include(i => i.Days)
                .Where(w => w.Id == tournamentDivisionId)
                .FirstOrDefaultAsync();
            if (division == null) return;

            var startDate = division.Days.OrderBy(o => o.Date).First().Date;

            foreach(var team in division.Teams)
            {
                foreach(var player in team.Players)
                {
                    var query = _db.TournamentTeamMembers
                        .Where(w => w.PlayerProfileId == player.PlayerProfileId)
                        .Where(w => w.DtEarned >= startDate);

                    if (division.DtPointCutoff.HasValue)
                        query = query.Where(w => w.DtEarned <= division.DtPointCutoff);

                    player.VblSeedingPoints = await query.SumAsync(s => s.VblTotalPointsEarned);
                }
            }
            await _db.SaveChangesAsync();
        }
        public async Task LockTournamentResults(int tournamentId)
        {
            var divisionIds = await _db.TournamentDivisions
                .Where(w => w.TournamentId == tournamentId)
                .Select(s => s.Id)
                .ToListAsync();

            foreach(var id in divisionIds)
            {
                if(await _db.TournamentTeams.Where(a => a.TournamentDivisionId == id && !a.IsDeleted).AnyAsync())
                    await LockResults(id);
            }
            var tournament = await _db.Tournaments.FindAsync(tournamentId);
            tournament.StatusId = (int)TournamentStatus.Complete;
            await _db.SaveChangesAsync();
        }
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
                .Where(w => w.DivisionId == division.DivisionId && w.SanctioningBodyId == division.SanctioningBodyId)
                .ToListAsync();
            if (!basePoints.Any()) return;

            var teamMultiplier = await _db.TeamCountMultipliers
                .Where(w => w.SanctioningBodyId == division.SanctioningBodyId)
                .OrderBy(o => o.TeamCap)
                .FirstOrDefaultAsync(f => f.TeamCap >= division.Teams.Count());

            var bottomAwarded = division.Teams.Max(m => m.Finish);
            var nextPlace = basePoints
                    .OrderBy(o => o.Finish)
                    .Where(w => w.Finish > bottomAwarded)
                    .First().Finish;
            foreach (var team in division.Teams.Where(w => !w.IsDeleted && w.Finish == null))
            {
                team.Finish = nextPlace;
            }
            foreach (var team in division.Teams.Where(w => !w.IsDeleted))
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
                    player.SanctioningBodyId = division.SanctioningBodyId;
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

                    if(points.PerTeam)
                    {
                        player.Multipliers.Add(new PointValueMultiplier
                        {
                            Type = "PerTeamMultiplier",
                            Description = "Points awarded are paer team.",
                            Value = .5,
                            TournamentTeamMember = player
                        });
                        player.VblTotalPointsEarned = player.VblTotalPointsEarned * .5;
                    }
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
