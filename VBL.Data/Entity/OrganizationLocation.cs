using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationLocation : TrackedEntityBase
    {
        public int OrganizationId { get; set; }
        public int LocationId { get; set; }

        public Organization Organization { get; set; }
        public Location Location { get; set; }
    }
}
