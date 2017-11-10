using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserPhoneNumber: TrackedEntityBase
    {
        public string UserId { get; set; }
        public string PhoneNumberId { get; set; }

        public ApplicationUser User { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
    }
}
