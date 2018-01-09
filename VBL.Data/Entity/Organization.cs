using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class Organization :TrackedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }

        public string WebsiteUrl { get; set; }
        public string Contact { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Snapchat { get; set; }

        ///Add partners
        ///
        
        public List<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();
        public List<OrganizationPhoto> Photos { get; set; } = new List<OrganizationPhoto>();
        public OrganizationTournamentDefaults TournamentDefaults { get; set; }
    }
}
