using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class PointValueDTO
    {
        public int DivisionId { get; set; }
        public string Division { get; set; }
        public int Finish { get; set; }
        public int Points { get; set; }
    }

    public class PointValueProfile: Profile
    {
        public PointValueProfile()
        {
            CreateMap<PointValue, PointValueDTO>()
                .ForMember(d => d.Division, opt => opt.MapFrom(s => s.Division.Name))
                .ForMember(d => d.DivisionId, opt => opt.MapFrom(s => s.Division.Id))
                ;
        }
    }
}
