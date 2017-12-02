using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class Phone : TrackedEntityBase
    {
        [Key]
        public string Number { get; set; }

        public bool IsPublic { get; set; }
        public bool IsSMS { get; set; }
        public bool IsVerified { get; set; }

        public UserPhone UserPhone { get; set; }
    }
}
