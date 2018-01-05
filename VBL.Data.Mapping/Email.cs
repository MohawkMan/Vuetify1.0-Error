using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class EmailDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public bool IsPublic { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<UserEmail, EmailDTO>()
                .ReverseMap();

        }
    }
}
