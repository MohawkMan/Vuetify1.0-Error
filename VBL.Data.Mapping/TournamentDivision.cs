using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentDivisionDTO
    {
        public int Id { get; set; }
        public int? MinTeams { get; set; }
        public int? MaxTeams { get; set; }
        public byte NumOfPlayers { get; set; }
        public byte NumAllowedOnRoster { get; set; }

        public OptionDTO AgeType { get; set; }
        public Option2DTO Gender { get; set; }
        public Option2DTO Division { get; set; }
        public List<TournamentDayDTO> Days { get; set; } = new List<TournamentDayDTO>();
        public OptionDTO Location { get; set; }
        public List<TournamentRegistrationWindowDTO> RegistrationWindows { get; set; } = new List<TournamentRegistrationWindowDTO>();
    }

    public class TournamentDivisionProfile : Profile
    {
        public TournamentDivisionProfile()
        {
            CreateMap<TournamentDivision, TournamentDivisionDTO>()
                .ReverseMap()
                .ForMember(s => s.AgeTypeId, opt => opt.MapFrom(d => d.AgeType.Id))
                .ForMember(s =>s.AgeType, opt => opt.Ignore())
                .ForMember(s => s.GenderId, opt => opt.MapFrom(d => d.Gender.Id))
                .ForMember(s => s.Gender, opt => opt.Ignore())
                .ForMember(s => s.DivisionId, opt => opt.MapFrom(d => d.Division.Id))
                .ForMember(s => s.Division, opt => opt.Ignore())
                .ForMember(s => s.LocationId, opt => opt.MapFrom(d => d.Location.Id))
                .ForMember(s => s.Location, opt => opt.Ignore());
        }
    }
}
