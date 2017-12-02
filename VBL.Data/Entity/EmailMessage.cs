using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class EmailMessage : TrackedEntityBase
    {
        public int Id { get; set; }
        public int? FromEmailId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Email FromEmail { get; set; }
    }
}
