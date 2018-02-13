using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationInfo: TrackedEntityBase
    {
        public int Id { get; set; }
        public int TournamentDivisionId { get; set; }

        public string Fields { get; set; }
        public string RequiredFields { get; set; }

        public List<string> FieldList => string.IsNullOrWhiteSpace(Fields) ? new List<string>() : Fields.Split(',').ToList();
        public List<string> RequiredFieldList => string.IsNullOrWhiteSpace(RequiredFields) ? new List<string>() : RequiredFields.Split(',').ToList();
    }
}
