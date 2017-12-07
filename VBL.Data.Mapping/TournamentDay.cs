using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentDayDTO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string DateFormatted { get; set; }
        public string CheckInTime { get; set; }
        public string PlayTime { get; set; }
    }

    public class TournamentDayProfile: Profile
    {
        public TournamentDayProfile()
        {
            CreateMap<TournamentDay, TournamentDayDTO>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
                .ForMember(d => d.DateFormatted, opt => opt.MapFrom(s => s.Date.ToString("MM/dd/yyyy")))
                .ReverseMap();
        }
    }
}
