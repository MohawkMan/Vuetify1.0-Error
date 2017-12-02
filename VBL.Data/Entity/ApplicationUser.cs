using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace VBL.Data
{
    public partial class ApplicationUser: IdentityUser<int>, ITrackedEntity
    {
        //Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string RegistrationProvider { get; set; }
        public string LastLoginProvider { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DtCreated { get; set; }
        public int? UserIdCreated { get; set; }
        public ApplicationUser UserCreated { get; set; }
        public DateTime? DtModified { get; set; }
        public int? UserIdModified { get; set; }
        public ApplicationUser UserModified { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }


        //Collections
        public List<UserPhone> UserPhones { get; set; } = new List<UserPhone>();
        public List<UserEmail> UserEmails { get; set; } = new List<UserEmail>();
        public List<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();
        public List<UserNotification> Notifications { get; set; } = new List<UserNotification>();

        public List<Organization> UserOrganizations => OrganizationMembers.Select(ou => ou.Organization).ToList();
    }
}
