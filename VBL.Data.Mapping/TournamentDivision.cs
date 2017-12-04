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

        public OptionDTO AgeType { get; set; }
        public OptionDTO Gender { get; set; }
        public OptionDTO Division { get; set; }
        public List<TournamentDayDTO> Days { get; set; } = new List<TournamentDayDTO>();
        public OptionDTO Location { get; set; }
        public List<TournamentRegistrationWindowDTO> RegistrationWindows { get; set; } = new List<TournamentRegistrationWindowDTO>();
    }

    public class TournamentDivisionProfile : Profile
    {
        public TournamentDivisionProfile()
        {
            CreateMap<TournamentDivision, TournamentDivisionDTO>();
        }
    }
}
