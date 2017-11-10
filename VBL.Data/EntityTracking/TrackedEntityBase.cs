using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class TrackedEntityBase : ITrackedEntity
    {
        public DateTime? DtCreated { get; set; }
        public string UserIdCreated { get; set; }
        public ApplicationUser UserCreated { get; set; }

        [Timestamp]
        public byte[] DtModified { get; set; }
        public string UserIdModified { get; set; }
        public ApplicationUser UserModified { get; set; }
    }
}
