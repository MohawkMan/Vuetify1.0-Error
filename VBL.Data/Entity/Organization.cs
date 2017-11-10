using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Organization :TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool Public { get; set; }

        public List<OrganizationUser> OrganizationUser { get; } = new List<OrganizationUser>();
    }
}
