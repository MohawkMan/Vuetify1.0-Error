using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VBL.Data;

namespace VBL.Data.Mapping
{
    public partial class TournamentRegistrationInfoDTO
    {
        public int Id { get; set; }

        public List<string> Fields { get; set; }
        public List<string> RequiredFields { get; set; }
    }

    public class TournamentRegistrationInfoProfile : Profile
    {
        public TournamentRegistrationInfoProfile()
        {
            CreateMap<TournamentRegistrationInfo, TournamentRegistrationInfoDTO>()
                .ForMember(d => d.Fields, opt => opt.MapFrom(s => s.FieldList))
                .ForMember(d => d.RequiredFields, opt => opt.MapFrom(s => s.RequiredFieldList))
                .ReverseMap()
                .ForMember(d => d.Fields, opt => opt.MapFrom(s => string.Join(",", s.Fields)))
                .ForMember(d => d.RequiredFields, opt => opt.MapFrom(s => string.Join(",", s.RequiredFields)))
                ;
        }
    }
}
