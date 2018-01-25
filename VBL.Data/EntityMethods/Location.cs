using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Location
    {
        public string Offset
        {
            get
            {
                var offset = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName).GetUtcOffset(DateTime.UtcNow);
                var num = offset.ToString(@"hh\:mm");
                var sign = offset > TimeSpan.Zero ? "+" : "-";
                return $"{sign}{num}";
            }
        }
    }
}
