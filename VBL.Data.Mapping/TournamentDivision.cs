﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string SanctioningBodyId { get; set; }
        public string EmailNote { get; set; }
        public int? TournamentDirectorUserId { get; set; }
        public DateTime DtRefundCutoff { get; set; }

        public string Offset { get; set; }

        public OptionDTO AgeType { get; set; }
        public Option2DTO Gender { get; set; }
        public Option2DTO Division { get; set; }
        public List<TournamentDayDTO> Days { get; set; } = new List<TournamentDayDTO>();
        public LocationDTO Location { get; set; }
        public List<TournamentRegistrationWindowDTO> RegistrationWindows { get; set; } = new List<TournamentRegistrationWindowDTO>();
        public List<TournamentTeamDTO> Teams { get; set; } = new List<TournamentTeamDTO>();
        public TournamentRegistrationInfoDTO RegistrationFields { get; set; }
    }

    public class TournamentDivisionProfile : Profile
    {
        public TournamentDivisionProfile()
        {
            CreateMap<TournamentDivision, TournamentDivisionDTO>()
                .ForMember(d => d.Teams, opt => opt.MapFrom(s =>s.Teams.Where(w => !w.IsDeleted)))
                .ForMember(s => s.Offset, opt => opt.MapFrom(d => TimeZoneInfo.FindSystemTimeZoneById(d.Location.TimeZoneName).GetUtcOffset(DateTime.Now)))
                .ReverseMap()
                .ForMember(s => s.AgeTypeId, opt => opt.MapFrom(d => d.AgeType.Id))
                .ForMember(s => s.AgeType, opt => opt.Ignore())
                .ForMember(s => s.GenderId, opt => opt.MapFrom(d => d.Gender.Id))
                .ForMember(s => s.Gender, opt => opt.Ignore())
                .ForMember(s => s.DivisionId, opt => opt.MapFrom(d => d.Division.Id))
                .ForMember(s => s.Division, opt => opt.Ignore())
                .ForMember(s => s.LocationId, opt => opt.MapFrom(d => d.Location.Id))
                .ForMember(s => s.Location, opt => opt.Ignore());
        }
    }
}
