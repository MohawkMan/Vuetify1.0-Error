using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class LocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDTO>();

            CreateMap<OrganizationLocation, LocationDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Location.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Location.Name))
                ;
        }
    }
}
