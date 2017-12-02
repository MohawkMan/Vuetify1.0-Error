﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data
{
    public partial class Email : TrackedEntityBase
    {
        [Key]
        public string Address { get; set; }

        public bool IsPublic { get; set; }
        public bool IsVerified { get; set; }
    }
}
