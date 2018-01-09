using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentTeam : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentDivisionId { get; set; }
        public TournamentDivision TournamentDivision { get; set; }
        public int TournamentRegistrationId { get; set; }
        public TournamentRegistration TournamentRegistration { get; set; }
        
        public List<TournamentTeamMember> Players { get; set; } = new List<TournamentTeamMember>();

        public int? Seed { get; set; }
        public int? Finish { get; set; }
    }
}
