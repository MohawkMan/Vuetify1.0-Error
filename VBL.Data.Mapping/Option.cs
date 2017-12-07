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
    public partial class Option2DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AgeTypeId { get; set; }
    }

    public class OptionProfile: Profile
    {
        public OptionProfile()
        {
            CreateMap<AgeType, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Gender, Option2DTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Division, Option2DTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
            CreateMap<Location, OptionDTO>()
                .ReverseMap()
                .ForMember(s => s.Name, opt => opt.Ignore());
        }
    }
}
