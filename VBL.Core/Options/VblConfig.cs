using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Core
{
    public class VblConfig
    {
        public AppKeysConfig AppKeys { get; set; }
        public LinksConfig Links { get; set; }
        public JwtConfig Jwt { get; set; }
        public PayPalConfig PayPal { get; set; }
        public SparkPostConfig SparkPost { get; set; }
        public StripeConfig Stripe { get; set; }
        public string BaseURL { get; set; }

    }
}
