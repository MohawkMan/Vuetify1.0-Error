using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationLocation
    {
        public int OrganizationId { get; set; }
        public int LocationId { get; set; }

        public Organization Organization { get; set; }
        public Location Location { get; set; }
    }
}
