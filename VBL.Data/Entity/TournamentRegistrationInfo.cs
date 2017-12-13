using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationInfo: TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentDivisionId { get; set; }

        public string Fields { get; set; }
        public string RequiredFields { get; set; }

        public string[] FieldList => Fields.Split(',');
        public string[] RequiredFieldList => RequiredFields.Split(',');
    }
}
