using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PayPalPaymentResponse : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public string RespMsg { get; set; }
        public string SecureToken { get; set; }
        public string SecureTokenId { get; set; }

        public string Pnref { get; set; }//Payflow Gateway transaction ID
        public string Ppref { get; set; }//PayPal transaction ID of the payment
        public decimal Amt { get; set; }
        public string Acct { get; set; }
        public string Cvv2Match { get; set; }
        public string AuthCode { get; set; }
        public string CorrelationId { get; set; }
        public string TransTime { get; set; }
        public byte CardType { get; set; }

        public string Error
        {
            get
            {
                switch (Result)
                {
                    case "12":
                        return "Card declined";
                    case "23":
                        return "Invalid card number.";
                    case "24":
                        return "Invalid expiration date.";
                    case "25":
                        return "Invalid card type.";
                    case "112":
                        return "Invlid postal code.";
                    case "114":
                        return "Invalid Expiry Date or CVV number";
                }
                return null;
            }
        }
    }
    public enum PaypalCardTypes
    {
        Visa = 0,
        MasterCard = 1,
        Discover = 2,
        AmericanExpress = 3,
        DinersClub = 4,
        JCB = 5
    }
}
