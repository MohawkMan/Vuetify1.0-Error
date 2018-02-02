using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class PayPalToken : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public string RespMsg { get; set; }
        public string SecureToken { get; set; }
        public string SecureTokenId { get; set; }

        public int PayPalTransactionId { get; set; }
        public PayPalTransaction PayPalTransaction { get; set; }

        public PayPalToken()
        {

        }
        public PayPalToken(string rawResponse)
        {
            var nameValues = QueryHelpers.ParseQuery(rawResponse);
            if (nameValues.ContainsKey("RESULT"))
                Result = nameValues["RESULT"].FirstOrDefault();
            if (nameValues.ContainsKey("RESPMSG"))
                RespMsg = nameValues["RESPMSG"].FirstOrDefault();
            if (nameValues.ContainsKey("SECURETOKEN"))
                SecureToken = nameValues["SECURETOKEN"].FirstOrDefault();
            if (nameValues.ContainsKey("SECURETOKENID"))
                SecureTokenId = nameValues["SECURETOKENID"].FirstOrDefault();
        }
    }
}
