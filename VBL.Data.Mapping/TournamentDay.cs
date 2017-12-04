using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentDayDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CheckInTime { get; set; }
        public string PlayTime { get; set; }
    }

    public class TournamentDayProfile: Profile
    {
        public TournamentDayProfile()
        {
            CreateMap<TournamentDay, TournamentDayProfile>();
        }
    }
}
