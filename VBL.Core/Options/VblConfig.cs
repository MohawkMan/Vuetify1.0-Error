using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Core
{
    public class VblConfig
    {
        public AppKeysConfig AppKeys { get; set; }
        public JwtConfig Jwt { get; set; }
        public SparkPostConfig SparkPost { get; set; }
        public LinksConfig Links { get; set; }
    }
}
