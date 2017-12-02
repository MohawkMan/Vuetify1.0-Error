using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentDay : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentDivisionId { get; set; }
        public DateTime Date { get; set; }
        public string CheckInTime { get; set; }
        public string PlayTime { get; set; }

        public TournamentDivision TournamentDivision { get; set; }
    }
}
