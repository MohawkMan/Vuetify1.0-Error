using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class ApplicationUser: IdentityUser, ITrackedEntity
    {
        //Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string RegistrationProvider { get; set; }
        public string LastLoginProvider { get; set; }
        public bool Active { get; set; }
        public DateTime? DtCreated { get; set; }
        public string UserIdCreated { get; set; }
        public ApplicationUser UserCreated { get; set; }
        [Timestamp]
        public byte[] DtModified { get; set; }
        public string UserIdModified { get; set; }
        public ApplicationUser UserModified { get; set; }

        //Collections
        public List<UserPhoneNumber> UserPhoneNumbers { get; } = new List<UserPhoneNumber>();
        public List<UserEmail> UserEmails { get; } = new List<UserEmail>();
        public List<OrganizationUser> OrganizationUsers { get; } = new List<OrganizationUser>();
        public List<UserNotification> Notifications { get; } = new List<UserNotification>();

        public List<Organization> UserOrganizations => OrganizationUsers.Select(ou => ou.Organization).ToList();
    }
}
