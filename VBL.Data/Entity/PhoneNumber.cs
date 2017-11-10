using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class PhoneNumber : TrackedEntityBase
    {
        [Key]
        public string Number { get; set; }

        public bool Public { get; set; }
        public bool SMS { get; set; }
        public bool Verified { get; set; }
    }
}
