using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationEmail : TrackedEntityBase
    {
        public int Id { get; set; }
        public int? FromEmailId { get; set; }
        public int OrganizationId { get; set; }
        public int? TournamentDivisionId { get; set; }
        public int EmailMessageId { get; set; }
        public bool IsDefault { get; set; }

        public Organization Organization { get; set; }
        public TournamentDivision TournamentDivision { get; set; }
        public EmailMessage EmailMessage { get; set; }
        public Email FromEmail { get; set; }
    }
}
