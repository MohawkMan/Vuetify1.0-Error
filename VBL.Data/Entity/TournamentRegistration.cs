using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistration : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int TournamentDivisionId { get; set; }
        public List<TournamentRegistrationPlayer> Players { get; set; }
        public string TeamName { get; set; }
        public bool Confirmed { get; set; }
        public string PaymentType { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; }


        // Temporary > Add List<AddOnProduct>
        public int AddOnQty { get; set; }

        public TournamentDivision TournamentDivision { get; set; }
        public TournamentTeam TournamentTeam { get; set; }
    }
}
