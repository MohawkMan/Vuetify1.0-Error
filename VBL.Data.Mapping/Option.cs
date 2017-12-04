using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class OptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OptionProfile: Profile
    {
        public OptionProfile()
        {
            CreateMap<AgeType, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Gender, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Division, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Location, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
        }
    }
}
