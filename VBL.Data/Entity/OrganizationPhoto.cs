using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationPhoto : TrackedEntityBase
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Url { get; set; }
        public bool IsPublic { get; set; }
        public bool IsCover { get; set; }

        public Organization Organization { get; set; }
    }
}
