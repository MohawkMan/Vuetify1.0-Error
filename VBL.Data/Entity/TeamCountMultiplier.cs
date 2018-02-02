using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TeamCountMultiplier : TrackedEntityBase
    {
        public int Id { get; set; }
        public int TeamCap { get; set; }
        public double Multiplier { get; set; }
        public string Description { get; set; }
        public string SanctioningBodyId { get; set; }
    }
}
