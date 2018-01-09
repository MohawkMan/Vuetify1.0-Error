using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class PointValueDTO
    {
        public Option2DTO Division { get; set; }
        public int Finish { get; set; }
        public int Points { get; set; }
    }

    public class PointValueProfile: Profile
    {
        public PointValueProfile()
        {
            CreateMap<PointValue, PointValueDTO>();
        }
    }
}
