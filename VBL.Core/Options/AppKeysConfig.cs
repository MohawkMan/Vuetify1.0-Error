using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBL.Core
{
    public class AppKeysConfig
    {
        public string Jwt { get; set; }
        public string SparkPost { get; set; }
        public string PayPal { get; set; }
        public string Stripe { get; set; }
        public string StripeTest { get; set; }
        public string AAUUserId { get; set; }
        public string AAURecordSource { get; set; }
    }
}
