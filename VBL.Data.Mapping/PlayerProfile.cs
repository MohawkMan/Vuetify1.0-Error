using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VBL.Data.Mapping
{
    public partial class PlayerProfileDTO
    {
        public string VblId { get; set; }
        public string AauNumber { get; set; }
        public string AvpNumber { get; set; }
        public string CbvaNumber { get; set; }
        public string UsavNumber { get; set; }
        public string Club { get; set; }
        public bool Male { get; set; }
    }

    public class PlayerProfileProfile: Profile
    {
        public PlayerProfileProfile()
        {
            CreateMap<PlayerProfile, PlayerProfileDTO>()
                .ReverseMap();
        }
    }
}
