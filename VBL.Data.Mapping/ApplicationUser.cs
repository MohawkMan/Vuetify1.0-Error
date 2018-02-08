using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VBL.Data.Mapping
{
    public class ApplicationUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool Active { get; set; }
        public List<PhoneDTO> Phones { get; set; } = new List<PhoneDTO>();
        public List<EmailDTO> Emails { get; set; } = new List<EmailDTO>();
        public List<PageDTO> Pages { get; set; } = new List<PageDTO>();
    }
    public class ApplicationUserPublicDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PhoneDTO> Phones { get; set; } = new List<PhoneDTO>();
        public List<EmailDTO> Emails { get; set; } = new List<EmailDTO>();
    }

    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(d => d.Phones, o => o.MapFrom(s => s.UserPhones))
                .ForMember(d => d.Emails, o => o.MapFrom(s => s.UserEmails))
                .ForMember(d => d.Pages, o => o.MapFrom(s => s.OrganizationMemberships.Where(w => w.IsActive)));

            //CreateMap<ApplicationUser, ApplicationUserPublicDTO>()
            //    .ForMember(d => d.Phones, o => o.MapFrom(s => s.UserPhones.Where(w => w.IsPublic)))
            //    .ForMember(d => d.Emails, o => o.MapFrom(s => s.UserEmails.Where(w => w.IsPublic)))
            //    ;
        }
    }

}
