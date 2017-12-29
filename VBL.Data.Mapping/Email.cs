using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class EmailDTO
    {
        public string Address { get; set; }
        public bool Public { get; set; }
        public bool Verified { get; set; }
    }
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<UserEmail, EmailDTO>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Email.Address))
                .ForMember(d => d.Public, opt => opt.MapFrom(s => s.IsPublic))
                .ForMember(d => d.Verified, opt => opt.MapFrom(s => s.Email.IsVerified))
                .ReverseMap();
        }
    }
}
