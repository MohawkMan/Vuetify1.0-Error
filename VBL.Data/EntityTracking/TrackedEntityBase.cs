using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class TrackedEntityBase : ITrackedEntity
    {
        public DateTime? DtCreated { get; set; }
        public int? UserIdCreated { get; set; }
        public ApplicationUser UserCreated { get; set; }

        public DateTime? DtModified { get; set; }
        public int? UserIdModified { get; set; }
        public ApplicationUser UserModified { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
