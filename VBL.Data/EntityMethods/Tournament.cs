using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class Tournament
    {
        public string StartDate
        {
            get
            {
                var date = Divisions.Select(d => d.StartDate).Min();
                return date.HasValue ? date.Value.ToString("yyyy-MM-dd") : "";
            }
        }
        public string EndDate
        {
            get
            {
                var date = Divisions.Select(d => d.StartDate).Max();
                return date.HasValue ? date.Value.ToString("yyyy-MM-dd") : "";
            }
        }
        public List<string> Locations
        {
            get
            {
                return Divisions.Select(d => d.Location.Name).Distinct().ToList();
            }
        }
        public string SanctionedBy
        {
            get
            {
                return Divisions.Any(a => !string.IsNullOrWhiteSpace(a.SanctioningBodyId)) ?
                    Divisions.Select(s => s.SanctioningBodyId).First(f => !string.IsNullOrWhiteSpace(f)) : "";
            }
        }
    }
}
