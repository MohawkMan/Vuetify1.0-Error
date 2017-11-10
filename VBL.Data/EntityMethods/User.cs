using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class ApplicationUser
    {
        public void AddEmail(string emailAddress)
        {
            UserEmails.Add(new UserEmail
            {
                Email = new Email { Address = emailAddress }
            });
        }
        public void NewNotification(string msg, string onClick)
        {
            Notifications.Add(new UserNotification
            {
                Message = msg,
                OnClick = onClick
            });
        }
    }
}
