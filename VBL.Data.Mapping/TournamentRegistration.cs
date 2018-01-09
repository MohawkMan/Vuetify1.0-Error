using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentRegistrationDTO
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int TournamentDivisionId { get; set; }
        public List<TournamentRegistrationPlayerDTO> Players { get; set; } = new List<TournamentRegistrationPlayerDTO>();
        public string TeamName { get; set; }
        public bool Confirmed { get; set; }
        public string PaymentType { get; set; }

        // Temporary > Add List<AddOnProduct>
        public int AddOnQty { get; set; }
        public int Finish { get; set; }
    }

    public class TournamentRegistrationProfile : Profile
    {
        public TournamentRegistrationProfile()
        {
            CreateMap<TournamentRegistration, TournamentRegistrationDTO>()
                .ReverseMap();                
        }
    }
}
