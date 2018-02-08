using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Feedback : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
