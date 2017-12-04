using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentTeamMember : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentTeamId { get; set; }

        public double? Points { get; set; }
        public DateTime? PointLockDt { get; set; }
    }
}
