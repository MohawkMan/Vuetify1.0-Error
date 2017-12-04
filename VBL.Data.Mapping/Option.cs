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
            CreateMap<AgeType, OptionDTO>();
            CreateMap<Gender, OptionDTO>();
            CreateMap<Division, OptionDTO>();
            CreateMap<Location, OptionDTO>();
        }
    }
}
