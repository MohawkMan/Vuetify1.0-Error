using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class UserPhone
    {
        public string NumberFormatted => string.IsNullOrWhiteSpace(Number) ? "" : string.Format("({0}) {1}-{2}", Number.Substring(0, 3), Number.Substring(3, 3), Number.Substring(6));
    }
}
