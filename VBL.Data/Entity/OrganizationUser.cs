using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationUser : TrackedEntityBase
    {
        public int OrganizationId { get; set; }
        public string UserId { get; set; }
        public OranizationUserRoles Role { get; set; }
        public bool IsActive { get; set; }
        
        public Organization Organization { get; set; }
        public ApplicationUser User { get; set; }
    }
}
