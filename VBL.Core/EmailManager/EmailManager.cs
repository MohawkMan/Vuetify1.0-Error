using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VBL.Data;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace VBL.Core.EmailManager
{
    public partial class EmailManager
    {
        private readonly VBLDbContext _db;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        public EmailManager(VBLDbContext db, ILogger<EmailManager> logger, IEmailSender emailSender)
        {
            _db = db;
            _logger = logger;
        }

        [Queue("email")]
        public async Task SendTournamentRegistrationEmailsAsync(int tournamentRegistrationId)
        {
            var registration = await _db.TournamentRegistrations
                .Include(i => i.Players)
                .Include(i => i.TournamentDivision)
                    .ThenInclude(t => t.Tournament)
                .FirstOrDefaultAsync(f => f.Id == tournamentRegistrationId);


        }
    }
}
