using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistrationPlayer
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
