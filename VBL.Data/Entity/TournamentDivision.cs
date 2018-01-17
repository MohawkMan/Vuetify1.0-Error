using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentDivision : TrackedEntityBase
    {
        public int TournamentId { get; set; }

        public int Id { get; set; }
        public int? MinTeams { get; set; }
        public int? MaxTeams { get; set; }
        public byte NumOfPlayers { get; set; } = 2;
        public byte NumAllowedOnRoster { get; set; } = 2;
        public string SanctioningBodyId { get; set; }
        public string EmailNote { get; set; }
        public int? TournamentDirectorUserId { get; set; }
        public DateTime DtRefundCutoff { get; set; }

        public int? AgeTypeId { get; set; }
        public int? GenderId { get; set; }
        public int? DivisionId { get; set; }
        public int? LocationId { get; set; }

        public string Info { get; set; }
        public bool IsSanctioningBodyApproved { get; set; }
        public int? SparkPostEmailTemplateId { get; set; }



        public Tournament Tournament { get; set; }
        public ApplicationUser TournamentDirector { get; set; }

        public AgeType AgeType { get; set; }
        public Gender Gender { get; set; }
        public Division Division { get; set; }
        public List<TournamentDay> Days { get; set; } = new List<TournamentDay>();
        public Location Location { get; set; }
        public List<TournamentRegistrationWindow> RegistrationWindows { get; set; } = new List<TournamentRegistrationWindow>();
        public List<TournamentTeam> Teams { get; set; } = new List<TournamentTeam>();
        public SparkPostEmailTemplate SparkPostEmailTemplate { get; set; }
        public TournamentRegistrationInfo RegistrationFields { get; set; }
    }
}
