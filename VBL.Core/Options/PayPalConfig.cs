using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Core
{
    public partial class PayPalConfig
    {
        public string Partner { get; set; }
        public string Vendor { get; set; }
        public string User { get; set; }
        public string TokenUrlDev { get; set; }
        public string TransactionUrlDev { get; set; }
        public string TokenUrl { get; set; }
        public string TransactionUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
