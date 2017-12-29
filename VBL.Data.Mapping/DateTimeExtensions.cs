using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public static partial class DateTimeExtensions
    {
        public static string ToVblFormatted(this DateTime dt)
        {
            return string.Format("{0:dddd, MMMM dd}{1} {0: yyyy}", dt, dt.DaySuffix());
        }
        public static string DaySuffix(this DateTime dt)
        {
            switch (dt.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
