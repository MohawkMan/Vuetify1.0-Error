using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentTeamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Seed { get; set; }
        public int? Points { get; set; }
        public int? Finish { get; set; }
        //public List<TournamentTeamMember> Players { get; set; } = new List<TournamentTeamMember>();
    }
    public partial class TournamentTeamWithPlayersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AauSeedingPoints { get; set; }
        public int? AvpSeedingPoints { get; set; }
        public List<TournamentTeamMemberDTO> Players { get; set; } = new List<TournamentTeamMemberDTO>();
    }
    public class TournamentTeamProfile : Profile
    {
        public TournamentTeamProfile()
        {
            CreateMap<TournamentTeam, TournamentTeamWithPlayersDTO>()
                .ForMember(d => d.AauSeedingPoints, opt => opt.MapFrom(s => s.Players.Sum(x => x.AauSeedingPoints)))
                .ForMember(d => d.AvpSeedingPoints, opt => opt.MapFrom(s => s.Players.Sum(x => x.AvpSeedingPoints)));

            CreateMap<TournamentTeam, TournamentTeamDTO>()
                .ForMember(d => d.Points, opt => opt.MapFrom(s => Convert.ToInt32(Math.Round(s.Players.Sum(p => p.VblTotalPointsEarned.HasValue ? p.VblTotalPointsEarned.Value : 0), MidpointRounding.AwayFromZero))))
                .ReverseMap();

            CreateMap<TournamentRegistration, TournamentTeam>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Name, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.TeamName) ?
                    string.Join("/", s.Players.Select(p => p.LastName)) : s.TeamName))
                .ForMember(d => d.TournamentDivisionId, opt => opt.MapFrom(s => s.TournamentDivisionId))
                .ForMember(d => d.TournamentRegistrationId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Players, opt => opt.Ignore());

        }
    }
}
