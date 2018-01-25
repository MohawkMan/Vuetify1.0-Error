using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using VBL.Data.Mapping;

namespace VBL.Core
{
    public partial class TournamentManager
    {
        private async Task<TournamentRegistration> DoRegistration(TournamentRegistrationDTO dto, bool sendEmailConfirmation)
        {
            //Map info from VblId if provided
            await PreMapPlayersDTOAsync(dto);

            //Map Registration
            var registration = _mapper.Map<TournamentRegistration>(dto);
            //Map all players that did not register with VblId
            await MapPlayersAsync(registration);
            //Add Team Record
            AddTournamentTeam(registration);
            if (dto.Finish > 0)
            {
                registration.TournamentTeam.Finish = dto.Finish;
            }

            _db.TournamentRegistrations.Add(registration);
            await _db.SaveChangesAsync();

            if (sendEmailConfirmation)
            {
                BackgroundJob.Enqueue<EmailManager>(_emailManager => _emailManager.SendTournamentRegistrationEmailsAsync(registration.Id));
            }
            return registration;
        }
        private async Task PreMapPlayersDTOAsync (TournamentRegistrationDTO registration)
        {
            foreach(var player in registration.Players.Where(w => !string.IsNullOrWhiteSpace(w.VblId)))
            {
                //VblId was provided during registration... map all fields from player profile
                var profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.VblId == player.VblId);
                if(profile != null)
                {
                    _mapper.Map(profile, player);
                }
            }
        }
        private async Task MapPlayersAsync(TournamentRegistration registration)
        {
            foreach(var player in registration.Players.Where(w => string.IsNullOrWhiteSpace(w.VblId)))
            {
                //only those without a VblId
                PlayerProfile profile = null;
                var review = false;

                // AAU MATCH
                if (!string.IsNullOrWhiteSpace(player.AauNumber) && player.AauNumber != "0")
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.AauNumber == player.AauNumber);
                }
                // AVP MATCH
                if (profile == null && !string.IsNullOrWhiteSpace(player.AvpNumber) && player.AvpNumber != "0")
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.AvpNumber == player.AvpNumber);
                }
                // USAV MATCH
                if (profile == null && !string.IsNullOrWhiteSpace(player.UsavNumber) && player.UsavNumber != "0")
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.UsavNumber == player.UsavNumber);
                }
                // CBVA MATCH
                if (profile == null && !string.IsNullOrWhiteSpace(player.CbvaNumber) && player.CbvaNumber != "0")
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.CbvaNumber == player.CbvaNumber);
                }
                // EMAIL MATCH
                if (profile == null && !string.IsNullOrWhiteSpace(player.Email))
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.User.PrimaryEmail != null && f.User.PrimaryEmail.Address == player.Email);
                    review = profile != null;
                }
                // PHONE MATCH
                if (profile == null && !string.IsNullOrWhiteSpace(player.Phone))
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.User.PrimaryPhone != null && f.User.PrimaryPhone.Number == player.Phone);
                    review = profile != null;
                }
                if(profile == null)
                {
                    profile = await _db.PlayerProfiles.FirstOrDefaultAsync(f => f.FirstName == player.FirstName && f.LastName == player.LastName && f.Club == player.Club);
                    review = profile != null;
                }

                if (profile != null)
                {
                    player.Profile = profile;
                    player.NeedsMatchReview = review;
                }
                else
                {
                    profile = new PlayerProfile();
                    _mapper.Map(player, profile);
                    profile.Male = await GetMaleFlag(registration);
                    player.Profile = profile;
                }
            }

        }
        private async Task<bool> GetMaleFlag (TournamentRegistration registration)
        {
            if (!_db.Entry(registration).Reference(r => r.Division).IsLoaded)
                await _db.Entry(registration).Reference(r => r.Division).LoadAsync();
            if (!_db.Entry(registration.Division).Reference(r => r.Gender).IsLoaded)
                await _db.Entry(registration.Division).Reference(r => r.Gender).LoadAsync();

            return registration.Division.Gender.Male.HasValue ? registration.Division.Gender.Male.Value : false;

        }
        private void AddTournamentTeam(TournamentRegistration registration)
        {
            var team = new TournamentTeam
            {
                Name = string.IsNullOrWhiteSpace(registration.TeamName) ? string.Join("/", registration.Players.Select(p => p.FirstName + " " + p.LastName)) : registration.TeamName,
                TournamentDivisionId = registration.TournamentDivisionId,
                TournamentRegistration = registration
            };
            foreach (var player in registration.Players)
            {
                team.Players.Add(new TournamentTeamMember { PlayerProfile = player.Profile });
            }

            registration.TournamentTeam = team;
        }
    }
}
