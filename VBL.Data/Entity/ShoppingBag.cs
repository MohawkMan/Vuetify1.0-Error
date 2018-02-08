using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class ShoppingBag : TrackedEntityBase
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public List<ShoppingBagItem> Items { get; set; } = new List<ShoppingBagItem>();
        public StripePaymentToken PaymentToken { get; set; }
        public double Total { get; set; }
        public string EmailReceiptTo { get; set; }
    }
}
