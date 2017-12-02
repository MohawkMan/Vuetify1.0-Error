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
            CreateMap<Email, EmailDTO>()
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Public, opt => opt.MapFrom(s => s.IsPublic))
                .ForMember(d => d.Verified, opt => opt.MapFrom(s => s.IsVerified));
        }
    }
}
