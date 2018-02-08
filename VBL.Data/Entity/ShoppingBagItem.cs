using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class ShoppingBagItem : TrackedEntityBase
    {
        public int Id { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag Bag { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public string RawRegistrationData { get; set; }
        public string RawProductData { get; set; }

        public int? TournamentRegistrationId { get; set; }
        public TournamentRegistration TournamentRegistration { get; set; }
    }
}
