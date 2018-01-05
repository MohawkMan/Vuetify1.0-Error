using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class ApplicationUser
    {
        public string FullName => $"{FirstName} {LastName}";
        public UserEmail PrimaryEmail => UserEmails.Where(w => w.IsPrimary).OrderByDescending(o => o.DtModified).FirstOrDefault();
        public UserPhone PrimaryPhone => UserPhones.Where(w => w.IsPrimary).OrderByDescending(o => o.DtModified).FirstOrDefault();
        public List<string> AdditionalEmails => UserEmails.Where(w => w.IsPublic && w.IsVerified && !w.IsPrimary).Select(s => s.Address).ToList();
        public List<string> AdditionalPhones => UserPhones.Where(w => w.IsPublic && w.IsVerified && !w.IsPrimary).Select(s => s.NumberFormatted).ToList();

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
