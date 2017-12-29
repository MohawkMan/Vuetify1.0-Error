using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBL.Core
{
    public class SparkPostConfig
    {
        public string BaseUrl { get; set; } = "https://api.sparkpost.com/api/v1";
        public string SendingUrl { get; set; } = "https://api.sparkpost.com/api/v1/transmissions";
    }
}
