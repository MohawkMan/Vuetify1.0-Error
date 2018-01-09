using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class PageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsPublic { get; set; }
    }

    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<OrganizationMember, PageDTO>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.OrganizationId))
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Organization.Name))
                .ForMember(s => s.Username, opt => opt.MapFrom(d => d.Organization.Username))
                .ForMember(s => s.IsPublic, opt => opt.MapFrom(d => d.Organization.IsPublic))
                ;
        }
    }
}
