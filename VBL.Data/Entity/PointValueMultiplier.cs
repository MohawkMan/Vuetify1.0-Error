using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PointValueMultiplier : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentTeamMemberId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public TournamentTeamMember TournamentTeamMember { get; set; }
    }
}
