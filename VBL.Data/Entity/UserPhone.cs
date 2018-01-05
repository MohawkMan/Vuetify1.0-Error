using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserPhone: TrackedEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Number { get; set; }
        public bool IsPublic { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSMS { get; set; }

        public ApplicationUser User { get; set; }
    }
}
