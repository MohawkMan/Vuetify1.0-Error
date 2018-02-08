using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class StripePaymentToken : TrackedEntityBase
    {
        public string Id { get; set; }
        public string CardId { get; set; }
        public string CardLast4 { get; set; }
        public string CardBrand { get; set; }
        public string ClientIP { get; set; }
    }
}
