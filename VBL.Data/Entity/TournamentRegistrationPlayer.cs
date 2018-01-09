using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationPlayer : TrackedEntityBase
    {
        public int Id { get; set; }
        public string VblId { get; set; }
        public int PlayerProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AauNumber { get; set; }
        public string AvpNumber { get; set; }
        public string CbvaNumber { get; set; }
        public string UsavNumber { get; set; }
        public string Club { get; set; }

        public bool NeedsMatchReview { get; set; }

        public PlayerProfile Profile { get; set; }
    }
}
