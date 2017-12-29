using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBL.Core
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string ExpireDays { get; set; }
    }
}
