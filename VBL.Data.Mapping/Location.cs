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
        public string GoogleUrl { get; set; }
        public string Offset { get; set; }
    }
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDTO>()
                .ReverseMap();
        }
    }
}
