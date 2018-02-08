using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class StripeChargeRecord : TrackedEntityBase
    {
        public string Id { get; set; }
        public string RawResponse { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag ShoppingBag { get; set; }
    }
}
