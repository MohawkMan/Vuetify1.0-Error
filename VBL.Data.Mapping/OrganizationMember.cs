using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class OrganizationMemberDTO
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class OrganizationMemberProfile : Profile
    {
        public OrganizationMemberProfile()
        {
            CreateMap<OrganizationMember, OrganizationMemberDTO>()
                .ReverseMap();
        }
    }
}
