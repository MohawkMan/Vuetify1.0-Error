using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class OrganizationDTO
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

        public List<OrganizationMemberDTO> OrganizationMembers { get; set; } = new List<OrganizationMemberDTO>();
        public List<OrganizationPhotoDTO> Photos { get; set; } = new List<OrganizationPhotoDTO>();
    }

    public partial class OrganizationDTOSkinny
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }

    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationDTO>()
                .ReverseMap();

            CreateMap<Organization, OrganizationDTOSkinny>();
        }
    }
}
