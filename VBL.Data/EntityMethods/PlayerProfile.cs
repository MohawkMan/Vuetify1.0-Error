using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PlayerProfile
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
