using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserPhone: TrackedEntityBase
    {
        public int UserId { get; set; }
        public string PhoneId { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPrimary { get; set; }

        public ApplicationUser User { get; set; }
        public Phone Phone { get; set; }

    }
}
