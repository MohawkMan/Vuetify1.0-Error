using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Tournament : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int? TournamentDirectorUserId { get; set; }
        public string EmailNote { get; set; }
        public int? SparkPostEmailTemplateId { get; set; }

        public int OrganizationId { get; set; }
        public int? SanctioningBodyId { get; set; }

        public bool IsPublic { get; set; }
        public bool IsOrganizationApproved { get; set; }
        public bool IsSanctioningBodyApproved { get; set; }

        public List<TournamentDivision> Divisions { get; set; } = new List<TournamentDivision>();

        public Organization Organization { get; set; }
        public ApplicationUser TournamentDirector { get; set; }
        public SanctioningBody SanctioningBody { get; set; }
        public SparkPostEmailTemplate SparkPostEmailTemplate { get; set; }
    }
}
