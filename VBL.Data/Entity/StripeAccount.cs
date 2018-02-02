using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Entity
{
    public partial class StripeAccount
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
