using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Core
{
    public class SmtpOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public bool UseSsl { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string PreferredEncoding { get; set; }
        public string DefaultFromName { get; set; }
        public string DefaultFromAddress { get; set; }
    }
    public class SmtpSecrets
    {
        public string SmtpPassword { get; set; }
    }
}
