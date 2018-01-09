using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PointValue : TrackedEntityBase
    {
        public int Id { get; set; }
        public int Finish { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int Points { get; set; }
    }
}
