using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationPlayer : TrackedEntityBase
    {
        public int Id { get; set; }
        public int VblId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CbvaNumber { get; set; }
        public string UsavNumber { get; set; }
    }
}
