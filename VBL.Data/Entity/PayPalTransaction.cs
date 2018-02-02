using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class PayPalTransaction : TrackedEntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RawData { get; set; }
        public float Total { get; set; }

        public List<PayPalToken> Tokens { get; set; }
    }
}
