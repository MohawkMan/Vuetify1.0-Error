using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data.Mapping
{
    public class TournamentRegistrationEmailViewModel
    {
        public object Substitution_data { get; set; }
        public List<object> Recipients { get; set; }
    }
    public class TournamentRegistrationEmailSubData
    {
        public string OrganizerName { get; set; }
        public string TournamentName { get; set; }
        public TournamentRegistrationEmailStartDay StartDay { get; set; }
        public string Location { get; set; }
        public string TournamentLink { get; set; }
        public string Division { get; set; }
        public string DtRefund { get; set; }
        public string TdNote { get; set; }
        public ApplicationUserPublicDTO Td { get; set; }
    }
    public class TournamentRegistrationEmailTD
    {
        public string Fullname { get; set; }
        public string Firstname { get; set; }
        public List<string> PublicEmails { get; set; }
        public List<string> PublicPhones { get; set; }
    }
    public class TournamentRegistrationEmailStartDay
    {
        public string Date { get; set; }
        public string CheckInTime { get; set; }
        public string PlayTime { get; set; }

    }
    public class TournamentRegistrationEmailRecipient
    {
        public string Address { get; set; }
        public object Substitution_data { get; set; }
    }
    public class TournamentRegistrationEmailRecipientSubData
    {
        public string Firstname { get; set; }
        public string Teammates { get; set; }
    }

    public class TournamentRegistrationEmailViewModelProfile : Profile
    {
        public TournamentRegistrationEmailViewModelProfile()
        {
            CreateMap<TournamentRegistration, TournamentRegistrationEmailSubData>()
                .ForMember(d => d.OrganizerName, opt => opt.MapFrom(s => s.Organization.Name))
                .ForMember(d => d.TournamentName, opt => opt.MapFrom(s => s.Tournament.Name))
                .ForMember(d => d.StartDay, opt => opt.MapFrom(s => s.Day1))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location.Name))
                .ForMember(d => d.TournamentLink, opt => opt.MapFrom(s => $"volleyballlife.com/${s.Organization.Username}/tournament/${s.Tournament.Id}"))
                .ForMember(d => d.Division, opt => opt.MapFrom(s => $"${s.TournamentDivision.Gender.Name} ${s.TournamentDivision.Division.Name}"))
                .ForMember(d => d.DtRefund, opt => opt.MapFrom(s => s.TournamentDivision.DtRefundCutoff.ToVblFormatted()))
                .ForMember(d => d.Td, opt => opt.MapFrom(s => s.TD))
                .ForMember(d => d.TdNote, opt => opt.MapFrom(s => s.EmailNote))
                ;

            CreateMap<ApplicationUser, TournamentRegistrationEmailTD>();

            CreateMap<TournamentDay, TournamentRegistrationEmailStartDay>()
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.ToVblFormatted()))
                ;
        }
    }
}
