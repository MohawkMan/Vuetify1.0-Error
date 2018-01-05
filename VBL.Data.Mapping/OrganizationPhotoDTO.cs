using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class OrganizationPhotoDTO
    {
        public string Url { get; set; }
        public bool IsPublic { get; set; }
        public bool IsCover { get; set; }
    }

    public class OrganizationPhotoProfile : Profile
    {
        public OrganizationPhotoProfile()
        {
            CreateMap<OrganizationPhoto, OrganizationPhotoDTO>()
                .ReverseMap();
        }
    }
}
