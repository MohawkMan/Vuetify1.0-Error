using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace VBL.Data.Mapping
{
    public partial class PhoneDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool IsPublic { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSMS { get; set; }
    }
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<UserPhone, PhoneDTO>()
                .ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Number, opt => opt.Ignore());
        }
    }
}
