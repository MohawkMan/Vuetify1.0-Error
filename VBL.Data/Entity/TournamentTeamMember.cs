using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentTeamMember : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentTeamId { get; set; }
        public int PlayerProfileId { get; set; }

        public double? VblSeedingPoints { get; set; }
        public double? AauSeedingPoints { get; set; }
        public double? AvpSeedingPoints { get; set; }
        public double? UsavSeedingPoints { get; set; }
        public double? CbvaSeedingPoints { get; set; }
        public DateTime? DtPointLock { get; set; }

        public int? Finish { get; set; }
        public double? VblBasePointsEarned { get; set; }
        public double? VblTotalPointsEarned { get; set; }
        public double? OrganizationPointsEarned { get; set; }
        public DateTime? DtEarned { get; set; }
        public DateTime? DtFinalized { get; set; }

        public TournamentTeam TournamentTeam { get; set; }
        public PlayerProfile PlayerProfile { get; set; }
        public List<PointValueMultiplier> Multipliers { get; set; } = new List<PointValueMultiplier>();
    }
}
