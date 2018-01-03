using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using VBL.Data;

namespace VBL.Core
{
    public partial class EmailManager
    {
        [Queue("email")]
        public async Task SendTournamentRegistrationEmailsAsync(int tournamentRegistrationId)
        {
            var registration = await LoadTournamentRegistration(tournamentRegistrationId);
            var sprakPostContent = new
            {
                substitution_data = MapTournamentRegistrationData(registration),
                recipients = MapTournamentRegistrationRecipients(registration),
                content = await MapSparkPostTemplate(registration.EmailTemplate, "TournamentRegistration")
            };
            var result = await SparkPostSend(sprakPostContent);
        }

        private object MapTournamentRegistrationData(TournamentRegistration registration)
        {
            var tourney = registration.Tournament;
            var division = registration.TournamentDivision;
            var startDay = registration.Day1;
            var td = registration.TD;
            return new
            {
                FeedbackLink = _config.Links.Feedback,
                organizername = tourney.Organization.Name,
                tournamentname = tourney.Name,
                teamname = registration.TournamentTeam.Name,
                date = startDay.Date.ToVblFormatted(),
                checkin = startDay.CheckInTime,
                playstarts = startDay.PlayTime,
                location = division.Location.Name,
                tournamentlink = $"volleyballlife.com/{tourney.Organization.UserName}/tournament/{tourney.Id}",
                division = $"{division.Gender.Name} {division.Division.Name}",
                dtrefund = division.DtRefundCutoff.ToVblFormatted(),
                td = new
                {
                    fullname = td.FullName,
                    firstname = td.FirstName,
                    email = td.PrimaryEmail,
                    phone = td.PrimaryPhone,
                    addEmails = td.AdditionalEmails,
                    addPhones = td.AdditionalPhones,
                    message = registration.EmailNote
                }
            };
        }
        private List<object> MapTournamentRegistrationRecipients(TournamentRegistration registration)
        {
            var result = new List<object>();
            foreach(var player in registration.Players)
            {
                var teamMateNames = registration.Players.Where(w => w.Id != player.Id).Select(s => s.FullName).ToList();
                teamMateNames[teamMateNames.Count-1] = $"and {teamMateNames.Last()}";
                result.Add(new
                {
                    address = player.Email,
                    substitution_data = new {
                        firstname = player.FirstName,
                        teammates = string.Join(",", teamMateNames)
                    }
                });
            }
            return result;
        }
        private async Task<TournamentRegistration> LoadTournamentRegistration(int tournamentRegistrationId)
        {
            return await _db.TournamentRegistrations
                .Include(i => i.Players)
                .Include(i => i.TournamentTeam)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Tournament)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Tournament)
                        .ThenInclude(t => t.Organization)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Days)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Gender)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Division)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Location)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.TournamentDirector)
                        .ThenInclude(t => t.UserEmails)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.TournamentDirector)
                        .ThenInclude(t => t.UserPhones)
                            .ThenInclude(t => t.Phone)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Tournament)
                        .ThenInclude(t => t.TournamentDirector)
                            .ThenInclude(t => t.UserEmails)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Tournament)
                        .ThenInclude(t => t.TournamentDirector)
                            .ThenInclude(t => t.UserPhones)
                                .ThenInclude(t => t.Phone)
                .FirstOrDefaultAsync(f => f.Id == tournamentRegistrationId);
        }
    }
}
