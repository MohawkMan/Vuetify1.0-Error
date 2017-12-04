using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace VBL.Data.Mapping
{
    public partial class PhoneDTO
    {
        public string Number { get; set; }
        public bool Public { get; set; }
        public bool SMS { get; set; }
        public bool Verified { get; set; }
    }
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<UserPhone, PhoneDTO>()
                .ForMember(d => d.Number, opt => opt.MapFrom(s => s.PhoneId))
                .ForMember(d => d.Public, opt => opt.MapFrom(s => s.Phone.IsPublic))
                .ForMember(d => d.SMS, opt => opt.MapFrom(s => s.Phone.IsSMS))
                .ForMember(d => d.Verified, opt => opt.MapFrom(s => s.Phone.IsVerified))
                .ReverseMap();
        }
    }
}
