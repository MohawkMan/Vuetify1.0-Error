using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using VBL.Data;

namespace VBL.Core
{
    public partial class UserResult
    {
        public IdentityResult Result { get; set; }
        public ApplicationUser User { get; set; }
    }
}
