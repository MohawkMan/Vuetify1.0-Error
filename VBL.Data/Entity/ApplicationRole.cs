using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName)
        {
            Name = roleName;
        }
    }
}
