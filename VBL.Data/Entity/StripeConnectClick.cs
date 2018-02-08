using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class StripeConnectClick : TrackedEntityBase
    {
        public string Id { get; set; }
        public int ApplicationUserId { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Code { get; set; }
        public string Scope { get; set; }
        public int? StripeAuthTokenId { get; set; }
        public StripeAuthToken StripeAuthToken { get; set; }

        public StripeConnectClick()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
