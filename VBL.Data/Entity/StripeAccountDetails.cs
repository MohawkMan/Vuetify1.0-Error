using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class StripeAccountDetails : TrackedEntityBase
    {
        public string Id { get; set; }
        public string PayoutStatementDescriptor { get; set; }
        public bool PayoutsEnabled { get; set; }
        public string Type { get; set; }
        public string Timezone { get; set; }
        public string SupportUrl { get; set; }
        public string SupportPhone { get; set; }
        public string SupportEmail { get; set; }
        public string StatementDescriptor { get; set; }
        public string ProductDescription { get; set; }
        public string DisplayName { get; set; }
        public bool DetailsSubmitted { get; set; }
        public string DefaultCurrency { get; set; }
        public bool DebitNegativeBalances { get; set; }
        public string Country { get; set; }
        public bool ChargesEnabled { get; set; }
        public string BusinessUrl { get; set; }
        public string BusinessPrimaryColor { get; set; }
        public string BusinessName { get; set; }
        public string BusinessLogoFileId { get; set; }
        public string Object { get; set; }
        public string Email { get; set; }

        public int StripeAuthTokenId { get; set; }
        public StripeAuthToken StripeAuthToken { get; set; }
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public bool IsDefault { get; set; }
    }
}
