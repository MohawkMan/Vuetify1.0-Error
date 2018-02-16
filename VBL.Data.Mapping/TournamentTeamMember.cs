using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentTeamMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlayerProfileId { get; set; }
        public double? AauSeedingPoints { get; set; }
        public double? AvpSeedingPoints { get; set; }
    }

    public class TournamentTeamMemberProfile: Profile
    {
        public TournamentTeamMemberProfile()
        {
            CreateMap<TournamentTeamMember, TournamentTeamMemberDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.PlayerProfile.FullName));
        }
    }
}
