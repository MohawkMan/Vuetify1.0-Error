using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class OrganizationMember : TrackedEntityBase
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        
        public Organization Organization { get; set; }
        public ApplicationUser User { get; set; }
    }
}
