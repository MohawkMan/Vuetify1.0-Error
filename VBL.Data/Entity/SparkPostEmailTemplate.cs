using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class SparkPostEmailTemplate : TrackedEntityBase
    {
        public int Id { get; set; }
        public string TemplateId { get; set; }
        public string For { get; set; }
        public bool IsDraft { get; set; }
        public bool IsCurrent { get; set; }
    }
}
