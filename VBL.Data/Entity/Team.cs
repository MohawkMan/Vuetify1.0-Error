using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Team : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentDivisionId { get; set; }

        public string Name { get; set; }

        public List<TeamMember> Players { get; set; } = new List<TeamMember>();

        public int? Seed { get; set; }
        public double? Points { get; set; }
        public int? Finish { get; set; }

        public TournamentDivision TournamentDivision { get; set; }
    }
}
