using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class StripeAuthToken : TrackedEntityBase
    {
        public int Id { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
        public bool LiveMode { get; set; }
        public string StripeUserId { get; set; }
        public string StripePublishableKey { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }

        public StripeAccountDetails AccountDetails { get; set; }
    }
}
