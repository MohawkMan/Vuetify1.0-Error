using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PlayerProfile : TrackedEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VblId { get; set; }
        public string AauNumber { get; set; }
        public string AvpNumber { get; set; }
        public string CbvaNumber { get; set; }
        public string UsavNumber { get; set; }
        public string Club { get; set; }
        public bool Male { get; set; }

        public int? ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
