using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class TournamentRegistration
    {
        public Organization Organization => TournamentDivision.Tournament.Organization;
        public Tournament Tournament => TournamentDivision.Tournament;
        public TournamentDivision Division => TournamentDivision;
        public Location Location => TournamentDivision.Location;
        public TournamentDay Day1 => TournamentDivision.Days.OrderBy(o => o.Date).FirstOrDefault();
        public ApplicationUser TD => TournamentDivision.TournamentDirector ?? Tournament.TournamentDirector;
        public SparkPostEmailTemplate EmailTemplate => TournamentDivision.SparkPostEmailTemplate ?? Tournament.SparkPostEmailTemplate;
        public string EmailNote => !string.IsNullOrWhiteSpace(TournamentDivision.EmailNote) ? TournamentDivision.EmailNote :
                        !string.IsNullOrWhiteSpace(TournamentDivision.Tournament.EmailNote) ? TournamentDivision.Tournament.EmailNote :
                        TournamentDivision.Tournament.Organization.DefaultEmailNote;
    }
}
