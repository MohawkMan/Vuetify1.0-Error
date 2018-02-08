using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationTournamentDefaults : TrackedEntityBase
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }

        public string Description { get; set; }
        public string EmailNote { get; set; }
        public string ImageUrl { get; set; }
        public bool OneDay { get; set; }

        public int LocationId { get; set; }
        public int TournamentDirectorUserId { get; set; }
        public ApplicationUser TournamentDirector { get; set; }
    }
}
