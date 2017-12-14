using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationWindow : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentDivisionId { get; set; }
        public double Fee { get; set; }
        public bool FeeIsPerTeam { get; set; } = true;
        public DateTime? DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
        public bool IsEarly { get; set; }
        public bool IsLate { get; set; }
        public bool CanPayAtEvent { get; set; } = false;
        public bool CanProcessPayment { get; set; } = true;
    }
}
