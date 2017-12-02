using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentDivision : TrackedEntityBase
    {
        public int Id { get; set; }
        public byte AgeTypeId { get; set; }
        public byte GenderId { get; set; }
        public byte DivisionId { get; set; }
        public int LocationId { get; set; }
        public List<TournamentDay> Days { get; set; }

        public int SanctioningBodyId { get; set; }
        public bool IsSanctioningBodyApproved { get; set; }

        public int MinTeams { get; set; }
        public int MaxTeams { get; set; }

        public byte NumOfPlayers { get; set; } = 2;
        public byte NumAllowedOnRoster { get; set; } = 2;

        public double EntryFee { get; set; }
        public double EarlyEntryFee { get; set; }
        public double LateEntryFee { get; set; }

        public DateTime EarlyRegistrationStartDt { get; set; }
        public DateTime RegistrationStartDt { get; set; }
        public DateTime RegistrationLateDt { get; set; }
        public DateTime RegistrationEndDt { get; set; }
        public DateTime RefundEndDt { get; set; }

        public string Info { get; set; }
        public int? TournamentRegistrationEmailId { get; set; }
        //Registration info and checks

        public List<Team> Teams { get; set; } = new List<Team>();

        public AgeType AgeType { get; set; }
        public Gender Gender { get; set; }
        public Division Division { get; set; }
        public Location Location { get; set; }
        public TournamentRegistrationEmail TournamentRegistrationEmail { get; set; }
    }
}
