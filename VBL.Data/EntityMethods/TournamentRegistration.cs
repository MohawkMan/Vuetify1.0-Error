using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string EmailNote
        {
            get
            {
                if (Division == null)
                    throw new NullReferenceException("TournamentDivision can not be null");
                if (Tournament == null)
                    throw new NullReferenceException("Tournament can not be null");
                if (Organization == null)
                    throw new NullReferenceException("Organization can not be null");
                if (Organization.TournamentDefaults == null)
                    throw new NullReferenceException("Organization.TournamentDefaults can not be null");

                return !string.IsNullOrWhiteSpace(TournamentDivision.EmailNote) ? TournamentDivision.EmailNote :
                       !string.IsNullOrWhiteSpace(Tournament.EmailNote) ? Tournament.EmailNote :
                       !string.IsNullOrWhiteSpace(Organization.TournamentDefaults.EmailNote) ? Organization.TournamentDefaults.EmailNote :
                       "";
            }
        }

    }
}
