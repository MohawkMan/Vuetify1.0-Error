using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserNotification: TrackedEntityBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string OnClick { get; set; }
        public bool Seen { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
