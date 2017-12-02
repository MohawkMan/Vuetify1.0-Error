using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TeamMember : TrackedEntityBase
    {
        public int Id { get; set; }
        public int ApplicationUserID { get; set; }
        public double? Points { get; set; }
        public DateTime? PointLockDt { get; set; }
    }
}
