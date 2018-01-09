using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class PlayerPointsDTO
    {
        public int PlayerProfileId { get; set; }
        public string Name { get; set; }
        public int CurrentPoints { get; set; }
        public int Events { get; set; }
        //public int CurrentRank { get; set; }
    }

    //public class PlayerRankingProfile : Profile
    //{
    //    public PlayerRankingProfile()
    //    {
    //        CreateMap<TournamentTeamMember, PlayerRankingDTO>()
    //            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
    //    }
    //}
}
