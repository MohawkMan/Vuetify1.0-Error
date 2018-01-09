using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserPostalAddress: TrackedEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }


        public bool IsPublic { get; set; }
        public bool IsPrimary { get; set; }

        public ApplicationUser User { get; set; }

    }
}
