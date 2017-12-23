using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class EmailMessage : TrackedEntityBase
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string PlainTextMessage { get; set; }
        public string HtmlMessage { get; set; }
        public string ReplyTo { get; set; }
        public DateTime? DTSent { get; set; }

        public Email FromEmail { get; set; }
    }
}
