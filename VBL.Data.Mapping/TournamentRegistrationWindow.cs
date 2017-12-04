using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentRegistrationWindowDTO
    {
        public int Id { get; set; }
        public double Fee { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public bool IsEarly { get; set; }
        public bool IsLate { get; set; }
    }

    public class TournamentRegistrationWindowProfile : Profile
    {
        public TournamentRegistrationWindowProfile()
        {
            CreateMap<TournamentRegistrationWindow, TournamentRegistrationWindowDTO>();
        }
    }
}
