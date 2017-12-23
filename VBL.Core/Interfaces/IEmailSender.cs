using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace VBL.Core
{
    public interface IEmailSender
    {
        void SendEmail(int emailId);
    }
}
