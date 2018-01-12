using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data
{
    public partial class TeamCountMultiplierDTO
    {
        public string Multiplier { get; set; }
        public string Description { get; set; }
    }

    public class TeamCountMultiplierProfile : Profile
    {
        public TeamCountMultiplierProfile()
        {
            CreateMap<TeamCountMultiplier, TeamCountMultiplierDTO>()
                .ForMember(d => d.Multiplier, opt => opt.MapFrom(s => $"{(int)(s.Multiplier * 100)}%"));
        }
    }
}
