using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserEmail : TrackedEntityBase
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }

        public ApplicationUser User { get; set; }
        public Email Email { get; set; }
    }
}
