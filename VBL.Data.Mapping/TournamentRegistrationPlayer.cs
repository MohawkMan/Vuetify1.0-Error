using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class TournamentRegistrationPlayerDTO
    {
        public int Id { get; set; }
        public string VblId { get; set; }
        public int? PlayerProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string DobFormatted { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Aau { get; set; }
        public string Avp { get; set; }
        public string Cbva { get; set; }
        public string Usav { get; set; }
        public string Club { get; set; }
        public bool Valid { get; set; }
        public bool ValidNumber { get; set; } = true;
    }

    public class TournamentRegistrationPlayerProfile : Profile
    {
        public TournamentRegistrationPlayerProfile ()
        {
            CreateMap<PlayerProfile, TournamentRegistrationPlayerDTO>()
                .ForMember(d => d.PlayerProfileId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.User.PrimaryPhone == null ? null : s.User.PrimaryPhone.Number))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.PrimaryEmail == null ? null : s.User.PrimaryEmail.Address))
                .ForMember(d => d.Dob, opt => opt.MapFrom(s => s.User.Dob.HasValue ? s.User.Dob.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(d => d.DobFormatted, opt => opt.MapFrom(s => s.User.Dob.HasValue ? s.User.Dob.Value.ToString("MM/dd/yyyy") : null))
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.User.PrimaryPostalAddress == null ? null : s.User.PrimaryPostalAddress.City))
                .ForMember(d => d.State, opt => opt.MapFrom(s => s.User.PrimaryPostalAddress == null ? null : s.User.PrimaryPostalAddress.State))
                .ForMember(d => d.Aau, opt => opt.MapFrom(s => s.AauNumber))
                .ForMember(d => d.Avp, opt => opt.MapFrom(s => s.AvpNumber))
                .ForMember(d => d.Cbva, opt => opt.MapFrom(s => s.CbvaNumber))
                .ForMember(d => d.Usav, opt => opt.MapFrom(s => s.UsavNumber))
                .ForMember(d => d.Club, opt => opt.MapFrom(s => s.Club));

            CreateMap<TournamentRegistrationPlayer, PlayerProfile>();

            CreateMap<TournamentRegistrationPlayer, TournamentRegistrationPlayerDTO>()
                .ForMember(d => d.Aau, opt => opt.MapFrom(s => s.AauNumber))
                .ForMember(d => d.Avp, opt => opt.MapFrom(s => s.AvpNumber))
                .ForMember(d => d.Cbva, opt => opt.MapFrom(s => s.CbvaNumber))
                .ForMember(d => d.Dob, opt => opt.MapFrom(s => s.Dob.HasValue ? s.Dob.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(d => d.DobFormatted, opt => opt.MapFrom(s => s.Dob.HasValue ? s.Dob.Value.ToString("MM/dd/yyyy") : null))
                .ForMember(d => d.Usav, opt => opt.MapFrom(s => s.UsavNumber));

            CreateMap<TournamentRegistrationPlayerDTO, TournamentRegistrationPlayer>()
                .ForMember(d => d.VblId, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.VblId) ? null : s.VblId.Trim()))
                .ForMember(d => d.AauNumber, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Aau) ? null : s.Aau.Trim()))
                .ForMember(d => d.AvpNumber, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Avp) ? null : s.Avp.Trim()))
                .ForMember(d => d.CbvaNumber, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Cbva) ? null : s.Cbva.Trim()))
                .ForMember(d => d.UsavNumber, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.Usav) ? null : s.Usav.Trim()))
                .ForAllOtherMembers(opt => opt.IgnoreSourceWhenDefault())
                ;

            CreateMap<string, DateTime?>().ConvertUsing<StringToNullableDateTimeConverter>();
        }
    }

    public class StringToNullableDateTimeConverter : ITypeConverter<string, DateTime?>
    {
        public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
        {
            if (DateTime.TryParse(source, out DateTime result))
            {
                return result;
            }
            return null;
        }
    }
}
