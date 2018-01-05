using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Location : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public string GoogleUrl { get; set; }
    }
}
