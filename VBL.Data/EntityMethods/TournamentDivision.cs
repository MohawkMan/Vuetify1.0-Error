using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentDivision
    {
        public DateTime? StartDate
        {
            get
            {
                return Days.Select(d => d.Date).Min();
            }
        }
        public DateTime? EndDate
        {
            get
            {
                return Days.Select(d => d.Date).Max();
            }
        }
    }
}
